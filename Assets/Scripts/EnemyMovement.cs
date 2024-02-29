using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<GameObject> _patrolPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _minPointDistance;

    private int _isRunningHash = Animator.StringToHash("isRunning");

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private int _currentPointIndex;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentPointIndex = 0;
        _animator.SetBool(_isRunningHash, true);
    }
    
    private void FixedUpdate()
    {
        int nextPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
        
        Vector2 currentPointPosition = _patrolPoints[_currentPointIndex].transform.position;
        Vector2 nextPointPosition = _patrolPoints[nextPointIndex].transform.position;

        float speed = nextPointPosition.x > currentPointPosition.x
            ? _speed
            : -_speed;

        if (speed * transform.localScale.x < 0)
        {
            FlipSprite();
        }
        
        _rigidbody.velocity = new Vector2(speed, 0);

        if (Vector2.Distance(transform.position, nextPointPosition) < _minPointDistance)
        {
            _currentPointIndex = nextPointIndex;
        }
    }

    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
