using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _defaultSpeed = 3f;

    private Vector3 _moveDirection = Vector3.forward;
    private float _speed;
    private bool _isInitialized;

    private void Awake()
    {
        _speed = _defaultSpeed;
        _moveDirection = transform.forward;
    }

    public void Initialize(Vector3 moveDirection, float speed)
    {
        _moveDirection = moveDirection.normalized;
        _speed = speed;
        _isInitialized = true;
    }

    private void Update()
    {
        if (_isInitialized == false)
            return;

        transform.position += _moveDirection * _speed * Time.deltaTime;
    }
}