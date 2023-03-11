using System.Collections.Generic;
using UnityEngine;

namespace Levels.PathFinding
{
    public class AStarPathFinding
    {
        private readonly Tile[,] _tiles;

        public AStarPathFinding(Tile[,] tiles)
        {
            _tiles = tiles;
        }
        
        public List<Tile> FindPath(Tile startTile, Tile endTile)
        {
            List<Tile> openList = new List<Tile>();
            HashSet<Tile> closedSet = new HashSet<Tile>();
            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
            Dictionary<Tile, float> gCosts = new Dictionary<Tile, float>();
            Dictionary<Tile, float> fCosts = new Dictionary<Tile, float>();

            openList.Add(startTile);
            gCosts[startTile] = 0f;
            fCosts[startTile] = GetHeuristicCost(startTile, endTile);

            while (openList.Count > 0)
            {
                Tile currentTile = GetLowestFCostTile(openList, fCosts);
                openList.Remove(currentTile);

                if (currentTile == endTile)
                {
                    return ReconstructPath(cameFrom, endTile);
                }

                closedSet.Add(currentTile);

                foreach (Tile neighbor in GetNeighborTiles(currentTile))
                {
                    if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    float tentativeGCost = gCosts[currentTile] + GetDistanceCost(currentTile, neighbor);

                    if (!openList.Contains(neighbor) || tentativeGCost < gCosts[neighbor])
                    {
                        cameFrom[neighbor] = currentTile;
                        gCosts[neighbor] = tentativeGCost;
                        fCosts[neighbor] = tentativeGCost + GetHeuristicCost(neighbor, endTile);

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            return null;
        }
        
        private List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile endTile)
        {
            List<Tile> path = new List<Tile> {endTile};

            while (cameFrom.ContainsKey(endTile))
            {
                endTile = cameFrom[endTile];
                path.Insert(0, endTile);
            }

            return path;
        }

        private Tile GetLowestFCostTile(List<Tile> tiles, Dictionary<Tile, float> fCosts)
        {
            Tile lowestCostTile = tiles[0];
            float lowestCost = fCosts[lowestCostTile];

            foreach (Tile tile in tiles)
            {
                float tileCost = fCosts[tile];
                if (tileCost < lowestCost)
                {
                    lowestCost = tileCost;
                    lowestCostTile = tile;
                }
            }

            return lowestCostTile;
        }

        private float GetHeuristicCost(Tile fromTile, Tile toTile)
        {
            float dx = Mathf.Abs(toTile.gridPosition.x - fromTile.gridPosition.x);
            float dy = Mathf.Abs(toTile.gridPosition.y - fromTile.gridPosition.y);
            return Mathf.Sqrt(dx * dx + dy * dy);
        }

        private float GetDistanceCost(Tile fromTile, Tile toTile)
        {
            float dx = Mathf.Abs(toTile.gridPosition.x - fromTile.gridPosition.x);
            float dy = Mathf.Abs(toTile.gridPosition.y - fromTile.gridPosition.y);
            float straightCost = Mathf.Min(dx, dy);
            float diagonalCost = Mathf.Max(dx, dy) - straightCost;
            return straightCost * 1f + diagonalCost * Mathf.Sqrt(2f);
        }
        
        private List<Tile> GetNeighborTiles(Tile tile)
        {
            List<Tile> neighbors = new List<Tile>();

            int x = (int)tile.gridPosition.x;
            int y = (int)tile.gridPosition.y;

            // Check North
            if (y + 1 < _tiles.GetLength(1))
            {
                neighbors.Add(_tiles[x, y + 1]);
            }

            // Check East
            if (x + 1 < _tiles.GetLength(0))
            {
                neighbors.Add(_tiles[x + 1, y]);
            }

            // Check South
            if (y - 1 >= 0)
            {
                neighbors.Add(_tiles[x, y - 1]);
            }

            // Check West
            if (x - 1 >= 0)
            {
                neighbors.Add(_tiles[x - 1, y]);
            }

            return neighbors;
        }
    }
}