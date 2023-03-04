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

        private void Start()
        {
            Level firstLevelPrefab = levelPrefabs[0];
            Level firstLevel = Instantiate(firstLevelPrefab);

            Player player = Instantiate(playerPrefab);
            Transform playerTransform = player.transform;
            playerTransform.position = firstLevel.PlayerSpawn.position;

            virtualCamera.Follow = playerTransform;
        }
    }
}