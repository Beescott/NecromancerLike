using UnityEngine;

namespace Entities.AI.FSM.Behaviours
{
    [CreateAssetMenu(fileName = "ChaseBehaviour", menuName = "FSM/Behaviours/Chase", order = 0)]
    public class ChaseBehaviour : AStateBehaviour
    {
        public override void OnStateEnter(StateMachine stateMachine)
        {
            base.OnStateEnter(stateMachine);
            stateMachine.TargetEnemy.Speed = stateMachine.TargetEnemy.Speed;
            stateMachine.TargetEnemy.StoppingDistance = stateMachine.TargetEnemy.AttackRange;
        }

        public override void OnStateExit(StateMachine stateMachine)
        {
            
        }
    }
}