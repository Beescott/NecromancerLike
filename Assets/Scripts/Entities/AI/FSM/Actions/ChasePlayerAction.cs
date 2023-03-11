using UnityEngine;

namespace Entities.AI.FSM.Actions
{
    [CreateAssetMenu(fileName = "ChasePlayerAction", menuName = "FSM/Actions/ChasePlayer", order = 0)]
    public class ChasePlayerAction : AStateAction
    {
        public override void OnActionEnter(StateMachine stateMachine)
        {
            stateMachine.TargetEnemy.SetPathToPlayer();
            stateMachine.TargetEnemy.entityTimers.LastPathCalculation = Time.time;
        }

        public override void Execute(StateMachine stateMachine)
        {
            stateMachine.TargetEnemy.FollowPath();
            // recalculate path only every 0.5 seconds
            if (Time.time - stateMachine.TargetEnemy.entityTimers.LastPathCalculation < 0.5f)
                return;
            
            stateMachine.TargetEnemy.SetPathToPlayer();
            stateMachine.TargetEnemy.entityTimers.LastPathCalculation = Time.time;
        }
    }
}