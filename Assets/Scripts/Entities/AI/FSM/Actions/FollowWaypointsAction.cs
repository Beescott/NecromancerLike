using UnityEngine;

namespace Entities.AI.FSM.Actions
{
    [CreateAssetMenu(fileName = "FollowWaypointsAction", menuName = "FSM/Actions/FollowWaypoints", order = 0)]
    public class FollowWaypointsAction : AStateAction
    {
        private const float MinDistanceSecurity = 0.1f;
        
        public override void Execute(StateMachine stateMachine)
        {
            // Vector3 agentDestination = stateMachine.TargetEnemy.NavMeshAgent.destination;
            // Vector3 agentPosition = stateMachine.transform.position;
            //
            // if (Vector3.Distance(agentDestination, agentPosition) < MinDistanceSecurity)
            // {
            //     stateMachine.TargetEnemy.WaypointPatroller.OnWaypointReached();
            //     Vector3 nextWaypoint = stateMachine.TargetEnemy.WaypointPatroller.GetNextWaypoint();
            //     stateMachine.TargetEnemy.NavMeshAgent.SetDestination(nextWaypoint);
            // }
        }
    }
}