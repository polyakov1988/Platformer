using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";
    
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    
    private int _stateHash = Animator.StringToHash("state");
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private float _horizontalInput;
    private bool _isGrounded;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _isGrounded = true;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw(Horizontal);
        
        if (Input.GetButtonDown(Jump) && _isGrounded)
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);

        if (_isJumping)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

            _isGrounded = false;
            _isJumping = false;
        }
        
        UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
    }
    
    private void UpdateAnimationState()
    {
        MovementState state = _horizontalInput == 0 
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

        _animator.SetInteger(_stateHash, (int) state);
    }
    
    private enum MovementState
    {
        Idle, 
        Running, 
        Jumping, 
        Falling
    }
}

