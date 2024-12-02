using UnityEngine;

public class CameraFollow : MonoBehaviour, IService
{
    [SerializeField] private Vector3 _offset;
    private Transform _player;
    private float _smoothSpeed = 0.025f;

    public void SetPlayer(Transform transform)
    {
        _player = transform;
    }

    private void LateUpdate()
    {
        if (_player is not null)
        {
            Vector3 desiredPosition = _player.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void OfFoolowTarget()
    {
        _player = null;
    }
}