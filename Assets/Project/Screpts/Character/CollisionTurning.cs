using System;
using UnityEngine;

namespace Project.Screpts.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionTurning : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private float _initialRotationZ;
        private Vector2 _lastNormal = Vector2.up;
        private Transform _currentPlatform;
        private Vector3 _localPosition;
        private Quaternion _rotationOffset;

        private void Awake()
        {
            _initialRotationZ = transform.eulerAngles.z;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _currentPlatform = collision.collider.transform;
            _rb.simulated = false;
            Vector2 normal = collision.contacts[0].normal;
            RotateToNormal(normal);
            AddSpinToObject(collision);
            _localPosition = _currentPlatform.InverseTransformPoint(transform.position);
            _rotationOffset = Quaternion.Inverse(_currentPlatform.rotation) * transform.rotation;
        }

        private void RotateToNormal(Vector2 normal)
        {
            float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }


        private void AddSpinToObject(Collision2D collision)
        {
            Rigidbody2D otherRb = collision.rigidbody;

            if (otherRb != null && !otherRb.isKinematic)
            {
                Vector2 collisionPoint = collision.contacts[0].point;
                Vector2 center = otherRb.worldCenterOfMass;

                Vector2 impactDirection = (collisionPoint - center).normalized;
                float torqueDirection = Vector3.Cross(impactDirection, Vector3.forward).z > 0 ? 1f : -1f;

                otherRb.AddTorque(torqueDirection * 0.05f, ForceMode2D.Impulse);
            }
        }
        
        private void FixedUpdate()
        {
            // Если есть платформа, обновляем позицию и поворот котика
            if (_currentPlatform != null)
            {
                // Обновляем позицию котика
                transform.position = _currentPlatform.TransformPoint(_localPosition);

                // Обновляем поворот котика
                transform.rotation = _currentPlatform.rotation * _rotationOffset;
            }
        }
        
        public void Detach()
        {
            // Открепляем котика от платформы (например, при прыжке)
            _currentPlatform = null;
            _rb.simulated = true;
        }
    }
}