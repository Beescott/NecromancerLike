using General;
using UnityEngine;

namespace Entities.AI.FSM
{
    public abstract class AStateAction : ScriptableObject, IResettable
    {
        public virtual void OnActionEnter(StateMachine stateMachine) { }
        public abstract void Execute(StateMachine stateMachine);
        public virtual void Reset() { }
    }
}