using General;
using UnityEngine;

namespace Entities.Players
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange = 5f; // The range of the player's attack
        [SerializeField] private float attackRate = 1f; // The rate at which the player can attack (in seconds)
        [SerializeField] private float maxReach = 1f;
        [SerializeField] private float attackDamage = 1f;

        [SerializeField] private Transform reachTransform;
        [SerializeField] private LayerMask attackLayer;
        [SerializeField] private GameObject attackDirection;

        private Camera _mainCamera;
        private float _nextAttackTime = 0f;
        private Collider2D[] _colliders = new Collider2D[4];
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        
        private void Update()
        {
            Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            MoveReach(mousePos, direction);
                
            if (!Input.GetMouseButtonDown(0) || !(Time.time >= _nextAttackTime)) 
                return;

            attackDirection.transform.right = direction;

            int size = Physics2D.OverlapCircleNonAlloc(reachTransform.position, attackRange, _colliders, attackLayer);
            for (int i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent(out IDamageable damageable) == false)
                    return;
                
                damageable.TakeDamage(attackDamage);
            }

            _nextAttackTime = Time.time + attackRate;
        }

        private void MoveReach(Vector2 mousePose, Vector2 direction)
        {
            reachTransform.position = (Vector2)transform.position + direction * maxReach;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(reachTransform.position, attackRange);
        }
    }
}
