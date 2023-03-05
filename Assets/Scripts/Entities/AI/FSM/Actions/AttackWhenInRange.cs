using System.Collections.Generic;
using Entities.Enemies;
using Entities.Players;
using General;
using UnityEngine;

namespace Entities.AI.FSM.Actions
{
    [CreateAssetMenu(fileName = "AttackWhenInRange", menuName = "FSM/Actions/AttackWhenInRange", order = 0)]
    public class AttackWhenInRange : AStateAction
    {
        private readonly Dictionary<Enemy, float> _lastAttackTime = new Dictionary<Enemy, float>();

        public override void Execute(StateMachine stateMachine)
        {
            Player player = GameManager.Instance.Player;
            if (stateMachine.TargetEnemy.IsInAttackRange(player.transform.position) == false)
                return;
            
            if (_lastAttackTime.ContainsKey(stateMachine.TargetEnemy) == false)
            {
                _lastAttackTime.Add(stateMachine.TargetEnemy, Time.time - 1.0f);
                return;
            }

            float enemyAttackRate = stateMachine.TargetEnemy.AttackRate;
            if (Time.time - _lastAttackTime[stateMachine.TargetEnemy] >= enemyAttackRate == false) 
                return;
            
            _lastAttackTime[stateMachine.TargetEnemy] = Time.time;
            Attack(stateMachine.TargetEnemy, player);
        }

        private void Attack(Enemy enemy, IDamageable damageable)
        {
            enemy.DealDamage(damageable);
        }

        public override void Reset()
        {
            _lastAttackTime.Clear();
        }
    }
}
