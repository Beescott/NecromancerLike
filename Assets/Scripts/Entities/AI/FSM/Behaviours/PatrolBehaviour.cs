using UnityEngine;

namespace Entities.AI.FSM.Behaviours
{
    [CreateAssetMenu(fileName = "PatrolBehaviour", menuName = "FSM/Behaviours/Patrol", order = 0)]
    public class PatrolBehaviour : AStateBehaviour
    {
        [SerializeField] private float patrolSpeed = 2.0f;
        
        private const float StoppingDistance = 0.1f;

        public override void OnStateEnter(StateMachine stateMachine)
        {
            // stateMachine.TargetEnemy.NavMeshAgent.speed = patrolSpeed;
            // stateMachine.TargetEnemy.NavMeshAgent.stoppingDistance = StoppingDistance;
        }

        public override void OnStateExit(StateMachine stateMachine)
        {
            
        }
    }
}