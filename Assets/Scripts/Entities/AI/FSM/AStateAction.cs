using General;
using UnityEngine;

namespace Entities.AI.FSM
{
    public abstract class AStateAction : ScriptableObject, IResettable
    {
        public abstract void Execute(StateMachine stateMachine);
        public virtual void Reset() { }
    }
}