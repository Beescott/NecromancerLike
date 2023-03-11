using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI.FSM.Actions
{
    [CreateAssetMenu(fileName = "FollowWaypointsAction", menuName = "FSM/Actions/FollowWaypoints", order = 0)]
    public class FollowWaypointsAction : AStateAction
    {
        private const float MinDistanceSecurity = 0.1f;

        public override void OnActionEnter(StateMachine stateMachine)
        {
            stateMachine.TargetEnemy.Path.Clear();
            stateMachine.TargetEnemy.WaypointPatroller.Initialize();
            List<Vector3> waypointPositions = new List<Vector3>();
            foreach (Transform waypointTransform in stateMachine.TargetEnemy.WaypointPatroller.Waypoints)
                waypointPositions.Add(waypointTransform.position);
            
            stateMachine.TargetEnemy.SetPath(waypointPositions);
        }

        public override void Execute(StateMachine stateMachine)
        {
            Vector3 agentPosition = stateMachine.transform.position;
            bool hasNextWaypoint = stateMachine.TargetEnemy.TryGetNextWaypoint(out Vector3 nextWaypoint);
            if (hasNextWaypoint == false)
                return;
            
            stateMachine.TargetEnemy.FollowPath();
            if (Vector3.Distance(nextWaypoint, agentPosition) < MinDistanceSecurity)
            {
                stateMachine.TargetEnemy.WaypointPatroller.OnWaypointReached();
                stateMachine.TargetEnemy.Path.Dequeue();
            }
        }
    }
}