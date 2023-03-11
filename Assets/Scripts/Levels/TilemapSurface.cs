using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Levels
{
    public class TilemapSurface : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap walls;
        [SerializeField] private Tilemap grounds;

        [SerializeField] private List<Tile> allTiles;
        public Tile[,] Tiles { get; private set; }

        [ContextMenu("Generate Tiles")]
        public void GenerateTiles()
        {
            Tiles = null;
            allTiles.Clear();

            Tiles = GetTilesFromTilemap(grounds);
            Tile[,] wallTiles = GetTilesFromTilemap(walls, true);

            foreach (Tile tile in Tiles)
                allTiles.Add(tile);

            foreach (Tile wallTile in wallTiles)
            {
                Tile foundTile = allTiles.Find(tile => wallTile.worldPosition == tile.worldPosition);
                if (foundTile == null)
                    continue;

                foundTile.isWalkable &= wallTile.isWalkable;
            }
        }

        private static Tile[,] GetTilesFromTilemap(Tilemap tilemap, bool invertIsWalkable = false)
        {
            Vector3Int size = tilemap.size;
            Tile[,] resultMap = new Tile[size.x, size.y];
            
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++) 
            {
                for (int y = 0; y < bounds.size.y; y++) 
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    bool isWalkable = tile != null;
                    if (invertIsWalkable) isWalkable = !isWalkable;
                    
                    Vector2 gridPosition = new Vector2(x, y);
                    Vector3 worldPosition = tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    worldPosition += tilemap.origin;
                    resultMap[x, y] = new Tile(gridPosition, worldPosition, isWalkable);
                }
            }

            return resultMap;
        }

        public Tile GetTileFromWorld(Vector3 worldPosition)
        {
            Vector3Int gridPosition = grounds.WorldToCell(worldPosition - grounds.origin);
            return Tiles[gridPosition.x, gridPosition.y];
        }
        
        private void OnDrawGizmosSelected()
        {
            if (Tiles == null || Tiles.GetLength(0) == 0 || Tiles.GetLength(1) == 0)
                return;
            
            foreach (Tile tile in allTiles)
            {
                Vector3 cubeSize = new Vector3(grid.cellSize.x, grid.cellSize.y, 0.1f);
                
                if (tile.isWalkable)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(tile.worldPosition, cubeSize);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(tile.worldPosition, cubeSize);
                }
            }
        }
    }
}