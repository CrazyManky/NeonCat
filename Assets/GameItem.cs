using System;
using DG.Tweening;
using Services;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _spriteColision;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = ServiceLocator.Instance.GetService<AudioManager>();
    }

    public event Action OnColisiton;
    private bool _isOneColision = false;

    public void OnCollisionEnter2D(Collision2D other)
    {
        // Меняем спрайт при столкновении
        _spriteRenderer.sprite = _spriteColision;

        if (!_isOneColision)
        {
            _audioManager.PlayColisionActive();

            Vector2 collisionNormal = other.contacts[0].normal;
            float angle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg;
            float direction = Vector3.Cross(collisionNormal, Vector3.up).z >= 0 ? 1 : -1;
            float targetAngle = direction * Mathf.Abs(angle);
            transform.DORotate(new Vector3(0, 0, targetAngle), 50f).SetLoops(-1, LoopType.Yoyo);
            var newValue = !_isOneColision;
            _isOneColision = newValue;
            OnColisiton?.Invoke();
        }
    }
}