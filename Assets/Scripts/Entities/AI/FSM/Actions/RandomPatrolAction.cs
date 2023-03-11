using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI.FSM.Actions
{
    [CreateAssetMenu(fileName = "RandomPatrolAction", menuName = "FSM/Actions/RandomPatrol", order = 0)]
    public class RandomPatrolAction : AStateAction
    {
        [SerializeField] private float range;
        
        public override void OnActionEnter(StateMachine stateMachine)
        {
            stateMachine.TargetEnemy.Path.Clear();

            Vector2 randomPosition = GetRandomPosition(stateMachine);
            Queue<Vector3> path = stateMachine.TargetEnemy.GetPathTo(randomPosition);
            stateMachine.TargetEnemy.SetPath(path);
        }

        public override void Execute(StateMachine stateMachine)
        {
            Vector3 agentPosition = stateMachine.transform.position;
            bool hasNextWaypoint = stateMachine.TargetEnemy.TryGetNextWaypoint(out Vector3 nextWaypoint);
            if (hasNextWaypoint == false)
            {
                Vector2 randomPosition = GetRandomPosition(stateMachine);
                Queue<Vector3> path = stateMachine.TargetEnemy.GetPathTo(randomPosition);
                stateMachine.TargetEnemy.SetPath(path);
                return;
            }
            
            stateMachine.TargetEnemy.FollowPath();
        }

        private Vector2 GetRandomPosition(StateMachine stateMachine)
        {
            return new Vector2(
                stateMachine.TargetEnemy.SpawnPosition.x + Random.Range(0, range),
                stateMachine.TargetEnemy.SpawnPosition.y + Random.Range(0, range)
            );
        }
    }
}