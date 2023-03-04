using System;
using System.Collections.Generic;
using Entities.Enemies;
using Entities.Events;
using General;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour, IEventReceiver<OnEntityDeath>
    {
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private List<Enemy> enemies;

        public Transform PlayerSpawn => playerSpawn;
        
        private void Awake()
        {
            EventBus.Register(this);
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
    }
}