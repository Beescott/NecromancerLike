using UnityEngine;

namespace Entities.AI.FSM
{
    public abstract class AStateTransition : ScriptableObject
    {
        [SerializeField] protected AStateBehaviour targetState;
        public AStateBehaviour TargetState => targetState;
        
        public abstract bool IsTriggered(StateMachine stateMachine);
    }
}