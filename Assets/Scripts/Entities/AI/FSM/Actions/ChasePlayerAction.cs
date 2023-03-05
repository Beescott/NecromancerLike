using Entities.Players;
using General;
using UnityEngine;

namespace Entities.AI.FSM.Actions
{
    [CreateAssetMenu(fileName = "ChasePlayerAction", menuName = "FSM/Actions/ChasePlayer", order = 0)]
    public class ChasePlayerAction : AStateAction
    {
        public override void Execute(StateMachine stateMachine)
        {
            Player player = GameManager.Instance.Player;
            // stateMachine.TargetEnemy.NavMeshAgent.SetDestination(player.transform.position);
        }
    }
}