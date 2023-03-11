using UnityEngine;

namespace Entities.Players
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f; // speed of movement
        [SerializeField] private float wallSlideSpeed = 1f; // speed of sliding along a wall
        [SerializeField] private float wallSlideDuration = 0.2f; 
        
        [Header("Physics")]
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private LayerMask wallLayerMask;
    
        private Vector3 _transformMovement = Vector3.zero;
        private readonly Collider2D[] _colliderArray = new Collider2D[4];
    
        private float _wallSlideTime = 0f;
        private Vector3 _wallSlideDirection = Vector3.zero;

        private void OnValidate()
        {
            playerCollider ??= GetComponent<Collider2D>();
        }

        void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 movement = GetMovement(horizontalInput, verticalInput);
        
            _transformMovement = movement;
            transform.position += _transformMovement;
        }

        private Vector2 GetMovement(float horizontalInput, float verticalInput)
        {
            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            movement = movement.normalized * (moveSpeed * Time.deltaTime);

            if (CheckCollision(movement) == false)
            {
                _wallSlideTime = 0f;
                _wallSlideDirection = Vector3.zero;
                return movement;
            }
        
            if (_wallSlideTime <= 0f)
            {
                _wallSlideTime = wallSlideDuration;
                _wallSlideDirection = Vector3.Cross(Vector3.back, movement);
                movement = Vector3.zero;
            }
            else
            {
                movement = _wallSlideDirection * (wallSlideSpeed * Time.deltaTime);
            }

            return movement;
        }

        private bool CheckCollision(Vector2 movement)
        {
            Vector2 newPosition = (Vector2)transform.position + movement;
            newPosition += movement.normalized * 0.01f;

            int size = Physics2D.OverlapBoxNonAlloc(newPosition + playerCollider.offset, playerCollider.bounds.size, 0f, _colliderArray, wallLayerMask);
            for (int i = 0; i < size; i++)
            {
                if (_colliderArray[i] != playerCollider)
                    return true;
            }
            return false;
        }
    }
}
