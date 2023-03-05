using Entities.Players;
using General;
using UnityEngine;

namespace Entities.AI.FSM.Transitions
{
    [CreateAssetMenu(fileName = "LosePlayerTransition", menuName = "FSM/Transitions/LosePlayer", order = 0)]
    public class LosePlayerTransition : AStateTransition
    {
        public override bool IsTriggered(StateMachine stateMachine)
        {
            Player player = GameManager.Instance.Player;
            return stateMachine.TargetEnemy.IsInDetectionRange(player.transform.position) == false;
        }
    }
}