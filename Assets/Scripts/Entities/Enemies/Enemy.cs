using System.Collections.Generic;
using Entities.AI.FSM;
using Entities.AI.WaypointPatrol;
using General;
using Levels;
using UnityEngine;

namespace Entities.Enemies
{
    public class Enemy : Entity, IDamageDealer
    {
        [Header("AI")]
        [SerializeField] private StateMachine stateMachine;
        
        [Header("Navigation")]
        [SerializeField] private WaypointPatroller waypointPatroller;

        [Header("Detection")]
        [SerializeField] private float detectionRange = 5.0f;

        [Header("Attack")]
        [SerializeField] private float attackRate = 1.0f;
        [SerializeField] private float attackRange = 1.0f;
        [SerializeField] private float attackDamage = 1.0f;

        public WaypointPatroller WaypointPatroller => waypointPatroller;
        public float AttackRate => attackRate;
        public float AttackRange => attackRange;

        public float Speed { get; set; }
        public float StoppingDistance { get; set; }
        
        private Level _level;
        private Queue<Vector3> _path = new Queue<Vector3>();
        public Queue<Vector3> Path => _path;
        
        public Vector3 SpawnPosition { get; private set; }

        public void Initialize(Level level)
        {
            _level = level;
            SpawnPosition = transform.position;
            stateMachine.OnStart(this);
        }

        public void OnUpdate()
        {
            stateMachine.OnUpdate();
        }
        
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
            Debug.Log($"Dealing damage");
            damageable.TakeDamage(attackDamage);
        }

        public void SetPath(IEnumerable<Vector3> positions)
        {
            _path = new Queue<Vector3>(positions);
        }
        
        [ContextMenu("SetPathToPlayer")]
        public void SetPathToPlayer()
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = GameManager.Instance.Player.transform.position;

            _path = _level.GetPathTo(startPosition, endPosition);
        }

        public bool TryGetNextWaypoint(out Vector3 nextWaypoint)
        {
            if (_path.Count == 0)
            {
                nextWaypoint = Vector3.negativeInfinity;
                return false;
            }

            nextWaypoint = _path.Peek();
            return true;
        }

        public Queue<Vector3> GetPathTo(Vector3 destination)
        {
            return _level.GetPathTo(transform.position, destination);
        }

        public void FollowPath()
        {
            if (_path.Count == 0)
                return;
            
            if (Vector3.Distance(_path.Peek(), transform.position) < 0.1f)
                _path.Dequeue();
            
            if (_path.Count == 0)
                return;

            Vector3 movement = (_path.Peek() - transform.position).normalized * (Speed * Time.deltaTime);
            transform.position += movement;
        }

        private void OnDrawGizmos()
        {
            if (_path == null || _path.Count < 2)
                return;

            Gizmos.color = Color.green;
            List<Vector3> path = new List<Vector3>(_path);
            for (int i = 0; i < _path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i] + Vector3.back, path[i + 1] + Vector3.back);
            }
        }
    }
}