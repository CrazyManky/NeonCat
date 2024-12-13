using System;
using UnityEngine;

namespace Project.Screpts.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionTurning : MonoBehaviour
    {
        private Transform _currentPlatform;
        private Transform _lastPlatform;
        private Vector3 _localPosition;
        private Quaternion _rotationOffset;
        private Rigidbody2D _rb;
        private Collider2D _currentPlatformCollider;

        private float _reattachCooldown = 0.5f;
        private float _lastDetachTime;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Time.time - _lastDetachTime < _reattachCooldown)
            {
                if (collision.collider.transform == _lastPlatform)
                    return;
            }

            _currentPlatform = collision.collider.transform;
            _currentPlatformCollider = collision.collider;

            _localPosition = _currentPlatform.InverseTransformPoint(transform.position);

            _rotationOffset = Quaternion.Inverse(_currentPlatform.rotation) *
                              AlignToSurface(collision.contacts[0].normal);

            _rb.simulated = false;
        }

        private void FixedUpdate()
        {
            if (_currentPlatform != null)
            {
                transform.position = _currentPlatform.TransformPoint(_localPosition);

                transform.rotation = _currentPlatform.rotation * _rotationOffset;
            }
        }

        public void Detach()
        {
            _lastPlatform = _currentPlatform;
            _lastDetachTime = Time.time;

            _currentPlatform = null;
            _currentPlatformCollider = null;

            _rb.simulated = true;
        }

        public bool IsOnPlatform()
        {
            return _currentPlatform != null;
        }

        public Collider2D GetPlatformCollider()
        {
            return _currentPlatformCollider;
        }

        private Quaternion AlignToSurface(Vector2 normal)
        {
            float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            return transform.rotation;
        }
    }
}