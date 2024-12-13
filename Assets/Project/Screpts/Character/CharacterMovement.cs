using UnityEngine;

namespace Project.Screpts.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _horizontalForce = 10f;
        [SerializeField] private float _jumpAngle = 45f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CollisionTurning _collisionTurning;

        private static readonly Vector2 _originalGravity = new(0, -9.81f);

        public void JumpLeft()
        {
            AttemptJump(-1);
        }

        public void JumpRight()
        {
            AttemptJump(1);
        }

        private void AttemptJump(int directionMultiplier)
        {
            if (_collisionTurning.IsOnPlatform())
            {
                Vector2 jumpDirection = CalculateJumpDirection(directionMultiplier);
                
                if (WillCollideWithPlatform(jumpDirection))
                {
                    return; 
                }

                _collisionTurning.Detach();
            }
            
            ExecuteJump(directionMultiplier);
        }

        private void ExecuteJump(int directionMultiplier)
        {
            _rb.simulated = true;
            Physics2D.gravity = _originalGravity;

            Vector2 direction = CalculateJumpDirection(directionMultiplier);
            _rb.velocity = new Vector2(direction.x * _horizontalForce, direction.y * _jumpForce);
            
            _spriteRenderer.flipX = directionMultiplier > 0;
        }

        private Vector2 CalculateJumpDirection(int directionMultiplier)
        {
            float angleInRadians = _jumpAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(angleInRadians) * directionMultiplier;
            float y = Mathf.Sin(angleInRadians);

            return new Vector2(x, y).normalized;
        }

        private bool WillCollideWithPlatform(Vector2 jumpDirection)
        {
            Collider2D platformCollider = _collisionTurning.GetPlatformCollider();
            if (platformCollider == null) return false;
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, jumpDirection, 1f);
            
            return hit.collider != null && hit.collider == platformCollider;
        }
    }
}