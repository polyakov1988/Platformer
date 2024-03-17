using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string JumpButton = "Jump";
    
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _hitDistance;
    
    private int _stateHash = Animator.StringToHash("state");
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private Hero _hero;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isAttacking;
    private bool _isDead;

    private void OnEnable()
    {
        _hero.GetComponent<Health>().IsDead += SetDead;
    }

    private void OnDisable()
    {
        _hero.GetComponent<Health>().IsDead -= SetDead;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _hero = GetComponent<Hero>();
        _isGrounded = true;
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        
        _horizontalInput = Input.GetAxisRaw(Horizontal);
        
        if (Input.GetButtonDown(JumpButton) && _isGrounded)
        {
            _isJumping = true;
        }
        
        if (Input.GetButtonDown("Fire1") && _isGrounded)
        {
            _isAttacking = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isDead)
        {
            return;
        }
        
        _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);

        if (_isJumping)
        {
            Jump();
        }

        if (_isAttacking)
        {
            Attack();
        }
        
        UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

        _isGrounded = false;
        _isJumping = false;
    }

    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * (_renderer.flipX ? -1 : 1), _hitDistance, _enemyLayer);
            
        if (hit.collider && hit.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_hero.GetDamage);
        }
    }
    
    private void UpdateAnimationState()
    {
        MovementState state;

        if (_isAttacking)
        {
            state = MovementState.Punch;

            _isAttacking = false;
        }
        else
        {
            state = _horizontalInput == 0 
                ? MovementState.Idle 
                : MovementState.Running;
        
            _renderer.flipX = _horizontalInput > 0 == false && (_horizontalInput < 0 || _renderer.flipX);
        
            if (_rigidbody.velocity.y > 0f && _isGrounded == false)
            {
                state = MovementState.Jumping;
            }
            else if (_rigidbody.velocity.y < 0f && _isGrounded == false)
            {
                state = MovementState.Falling;
            }
        }
        
        _animator.SetInteger(_stateHash, (int) state);
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
        Jumping, 
        Falling,
        Punch,
        Death
    }
}

