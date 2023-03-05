using AI.WaypointPatrol;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity, IDamageDealer
    {
        [Header("Navigation")]
        [SerializeField] private WaypointPatroller waypointPatroller;
        [SerializeField] private float speed;

        [Header("Detection")]
        [SerializeField] private float detectionRange = 5.0f;

        [Header("Attack")]
        [SerializeField] private float attackRate = 1.0f;
        [SerializeField] private float attackRange = 1.0f;
        [SerializeField] private float attackDamage = 1.0f;

        public WaypointPatroller WaypointPatroller => waypointPatroller;
        public float Speed => speed;
        public float AttackRate => attackRate;
        public float AttackRange => attackRange;
        
        public bool IsInDetectionRange(Vector3 objectPosition)
        {
            return Vector3.Distance(objectPosition, transform.position) < detectionRange;
        }

        public bool IsInAttackRange(Vector3 objectPosition)
        {
            return Vector3.Distance(objectPosition, transform.position) < attackRange;
        }

        public void DealDamage(IDamageable damageable)
        {
            damageable.TakeDamage(attackDamage);
        }
    }
}