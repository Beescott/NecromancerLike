using System.Collections;
using System.Collections.Generic;
using Entities.Enemies;
using Entities.Events;
using General;
using Levels.PathFinding;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour, IEventReceiver<OnEntityDeath>
    {
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private TilemapSurface tilemapSurface;

        public Transform PlayerSpawn => playerSpawn;
        private AStarPathFinding _pathFinding;

        private void Awake()
        {
            EventBus.Register(this);
            tilemapSurface.GenerateTiles();
            _pathFinding = new AStarPathFinding(tilemapSurface.Tiles);
            
            foreach (Enemy enemy in enemies)
                enemy.Initialize(this);
        }

        public IEnumerator OnGameTick()
        {
            while (IsPlaying())
            {
                foreach (Enemy enemy in enemies)
                    enemy.OnUpdate();
                
                yield return null;
            }
        }

        private bool IsPlaying()
        {
            return enemies.Count > 0 && GameManager.Instance.Player.IsDead == false;
        }

        private void OnDestroy()
        {
            EventBus.UnRegister(this);
        }

        public void OnEvent(OnEntityDeath e)
        {
            if (e.Entity is Enemy enemy && enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }

        public Queue<Vector3> GetPathTo(Vector3 startPosition, Vector3 endPosition)
        {
            Tile startTile = tilemapSurface.GetTileFromWorld(startPosition);
            Tile endTile = tilemapSurface.GetTileFromWorld(endPosition);

            List<Tile> tilePath = _pathFinding.FindPath(startTile, endTile);
            Queue<Vector3> path = new Queue<Vector3>();
            foreach (Tile tile in tilePath)
                path.Enqueue(tile.worldPosition);

            return path;
        }
    }
}