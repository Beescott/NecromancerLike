using System;
using UnityEngine;

namespace Levels
{
    [Serializable]
    public class Tile
    {
        public Vector2 gridPosition;
        public Vector3 worldPosition;
        public bool isWalkable;

        public Tile(Vector2 gridPosition, Vector3 worldPosition, bool isWalkable)
        {
            this.gridPosition = gridPosition;
            this.worldPosition = worldPosition;
            this.isWalkable = isWalkable;
        }
    }
}