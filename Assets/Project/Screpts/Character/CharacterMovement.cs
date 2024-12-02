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

        public Rigidbody2D RB => _rb;

        public void JumpLeft()
        {
            _collisionTurning.Detach();
            _spriteRenderer.flipX = false;
            Jump(-1);
        }

        public void JumpRight()
        {
            _collisionTurning.Detach();
            _spriteRenderer.flipX = true;
            Jump(1);
        }

        private void Jump(int directionMultiplier)
        {
            _rb.simulated = true;
            Physics2D.gravity = _originalGravity;
            Vector2 direction = CalculateJumpDirection(directionMultiplier);

            _rb.velocity = Vector2.zero;
            _rb.velocity = new Vector2(direction.x * _horizontalForce, direction.y * _jumpForce);
        }

        private Vector2 CalculateJumpDirection(int directionMultiplier)
        {
            float angleInRadians = _jumpAngle * Mathf.Deg2Rad;

            float x = Mathf.Cos(angleInRadians) * directionMultiplier;
            float y = Mathf.Sin(angleInRadians);

            return new Vector2(x, y).normalized;
        }
    }
}