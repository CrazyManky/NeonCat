using System;
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
        _spriteRenderer.sprite = _spriteColision;
        if (!_isOneColision)
        {
            _audioManager.PlayColisionActive();
            var newValue = !_isOneColision;
            _isOneColision = newValue;
            OnColisiton?.Invoke();
        }
    }
}