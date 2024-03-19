using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _patrolPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _minPointDistance;
    [SerializeField] private float _hitDistance;
    [SerializeField] private LayerMask _heroLayer;

    private int _stateHash = Animator.StringToHash("state");

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Enemy _enemy;
    private int _currentPointIndex;
    private bool _isDead;
    private bool _canHit;
    private WaitForSeconds _hitTimeout;
    
    private void OnEnable()
    {
        _enemy.GetComponent<Health>().IsDead += SetDead;
    }

    private void OnDisable()
    {
        _enemy.GetComponent<Health>().IsDead -= SetDead;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _currentPointIndex = 0;
        _animator.SetInteger(_stateHash, (int) MovementState.Running);
        _canHit = true;
        _hitTimeout = new WaitForSeconds(1);
    }
    
    private void FixedUpdate()
    {
        if (_isDead)
        {
            return;
        }
        
        _animator.SetInteger(_stateHash, (int) MovementState.Running);
        
        int nextPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
        
        Vector2 currentPointPosition = _patrolPoints[_currentPointIndex].position;
        Vector2 nextPointPosition = _patrolPoints[nextPointIndex].position;
        
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, 
            Vector2.right * (_rigidbody.velocity.x < 0 ? -1 : 1), 
            Vector2.Distance(transform.position, nextPointPosition), 
            _heroLayer);
            
        if (raycast.collider && raycast.collider.TryGetComponent(out Hero hero))
        {
            ChaseHero(hero, raycast);
        }
        else
        {
            PatrolTerritory(currentPointPosition, nextPointPosition, nextPointIndex);
        }
    }

    private void ChaseHero(Hero hero, RaycastHit2D raycast)
    {
        float chaseSpeed = hero.transform.position.x > transform.position.x
            ? _chaseSpeed
            : -_chaseSpeed;
            
        _rigidbody.velocity = new Vector2(chaseSpeed, 0);

        if (raycast.distance < _hitDistance && _canHit)
        {
            _animator.SetInteger(_stateHash, (int) MovementState.Attack);
                
            hero.TakeDamage(_enemy.GetDamage);

            StartCoroutine(WaitAttackTimeout());
        }
    }

    private void PatrolTerritory(Vector2 currentPointPosition, Vector2 nextPointPosition, int nextPointIndex)
    {
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

    private IEnumerator WaitAttackTimeout()
    {
        _canHit = false;

        yield return _hitTimeout;

        _canHit = true;
    }

    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    private void SetDead()
    {
        _isDead = true;
        
        _animator.SetInteger(_stateHash, (int) MovementState.Death);
    }
    
    private enum MovementState
    {
        Idle, 
        Running, 
        Attack,
        Death
    }
}
