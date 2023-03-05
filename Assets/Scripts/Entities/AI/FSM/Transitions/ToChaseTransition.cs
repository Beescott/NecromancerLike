using Entities.Players;
using General;
using UnityEngine;

namespace Entities.AI.FSM.Transitions
{
    [CreateAssetMenu(fileName = "ToChaseTransition", menuName = "FSM/Transitions/ToChase", order = 0)]
    public class ToChaseTransition : AStateTransition
    {
        public override bool IsTriggered(StateMachine stateMachine)
        {
            Player player = GameManager.Instance.Player;
            return stateMachine.TargetEnemy.IsInDetectionRange(player.transform.position);
        }
    }
}