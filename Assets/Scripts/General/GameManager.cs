using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Entities.Players;
using Levels;
using UnityEngine;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levelPrefabs;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private static GameManager _instance;
        public static GameManager Instance => _instance;

        public Player Player { get; private set; }

        private void Awake()
        {
            _instance = this;
        }
        
        private void Start()
        {
            Level firstLevelPrefab = levelPrefabs[0];
            Level firstLevel = Instantiate(firstLevelPrefab);

            Player = Instantiate(playerPrefab);
            Transform playerTransform = Player.transform;
            playerTransform.position = firstLevel.PlayerSpawn.position;

            virtualCamera.Follow = playerTransform;
            StartCoroutine(StartLevel(firstLevel));
        }

        private IEnumerator StartLevel(Level level)
        {
            yield return level.OnGameTick();
        }
    }
}