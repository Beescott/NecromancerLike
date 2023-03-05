using Entities.Enemies;
using General.ReadOnlyAttributes;
using UnityEngine;

namespace Entities.AI.FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private AStateBehaviour initialState;

        [ReadOnly, SerializeField] private AStateBehaviour currentState;

        private Enemy _targetEnemy;
        public Enemy TargetEnemy => _targetEnemy;

        public void OnStart(Enemy targetEnemy)
        {
            _targetEnemy = targetEnemy;
            initialState.OnStateEnter(this);
            currentState = initialState;
        }

        public void OnUpdate()
        {
            currentState.OnUpdate(this);
        }
        
        public void ChangeState(AStateBehaviour newState)
        {
            if (currentState != null)
                currentState.OnStateExit(this);
            
            currentState = newState;
            currentState.OnStateEnter(this);
        }
    }
}