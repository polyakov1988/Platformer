using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private static readonly int State = Animator.StringToHash("state");
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private float _horizontalInput;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _isGrounded = true;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        
        _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

            _isGrounded = false;
        }
        
        UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
    }
    
    private void UpdateAnimationState()
    {
        MovementState state;

        if (_horizontalInput > 0f)
        {
            state = MovementState.Running;
            _renderer.flipX = false;
        }
        else if (_horizontalInput < 0f)
        {
            state = MovementState.Running;
            _renderer.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }

        if (_rigidbody.velocity.y > 0f && _isGrounded == false)
        {
            state = MovementState.Jumping;
        }
        else if (_rigidbody.velocity.y < 0f && _isGrounded == false)
        {
            state = MovementState.Falling;
        }

        _animator.SetInteger(State, (int) state);
    }
    
    private enum MovementState
    {
        Idle, 
        Running, 
        Jumping, 
        Falling
    }
}

