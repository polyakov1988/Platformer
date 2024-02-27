using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _pointA;
    [SerializeField] private GameObject _pointB;
    [SerializeField] private float _speed;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Transform _currentPoint;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentPoint = _pointB.transform;
        _animator.SetBool(IsRunning, true);
    }
    
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_currentPoint == _pointB.transform ? _speed : -_speed, 0);

        CheckCurrentPoint(_pointA, _pointB);
        CheckCurrentPoint(_pointB, _pointA);
    }

    private void CheckCurrentPoint(GameObject firstPoint, GameObject secondPoint)
    {
        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == firstPoint.transform)
        {
            FlipSprite();
            _currentPoint = secondPoint.transform;
        }
    }

    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
