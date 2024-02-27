using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class HeroMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _horizontalInput;
    private bool _isFacingRight;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _isFacingRight = true;
        _isGrounded = true;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        FlipSprite();
        
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            _isGrounded = false;
            _animator.SetBool(IsJumping, !_isGrounded);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_horizontalInput * _speed, _rigidbody.velocity.y);
        
        _animator.SetFloat(XVelocity, Math.Abs(_rigidbody.velocity.x));
        _animator.SetFloat(YVelocity, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isGrounded = true;
        _animator.SetBool(IsJumping, !_isGrounded);
    }

    private void FlipSprite()
    {
        if (_isFacingRight && _horizontalInput < 0f || !_isFacingRight && _horizontalInput > 0f)
        {
            _isFacingRight = !_isFacingRight;
            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            
            transform.localScale = localScale;
        }
    }
}

