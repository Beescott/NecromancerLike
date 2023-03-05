using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI.FSM
{
    public abstract class AStateBehaviour : ScriptableObject
    {
        [SerializeField] protected List<AStateAction> actions;
        [SerializeField] protected List<AStateTransition> transitions;

        public abstract void OnStateEnter(StateMachine stateMachine);
        public abstract void OnStateExit(StateMachine stateMachine);

        public virtual void OnUpdate(StateMachine stateMachine)
        {
            foreach (AStateAction action in actions)
                action.Execute(stateMachine);
            
            foreach (AStateTransition transition in transitions)
            {
                if (transition.IsTriggered(stateMachine) == false)
                    continue;
                
                stateMachine.ChangeState(transition.TargetState);
                break;
            }
        }
    }
}