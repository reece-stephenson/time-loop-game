using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField] private LayerMask _jumpableGround;
    public Rigidbody2D RigidBody { get => _rigidBody; }

    public bool IsDead { get; set; }

    private BoxCollider2D _boxCollider;

    private float movementDirection = 0f;

    [SerializeField]
    float _movementSpeed = 4f;

    [SerializeField]
    float _jumpHeight = 5f;

    public bool LockMovement { get; set; } = false;

    public Queue<MovementPair> _movements { get; set; }
    public Queue<CommonAnimationState> _animations { get; set; }

    public MovementState _movementState;

    private bool top;

    private bool isGravityFlipped;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _movements = new Queue<MovementPair>();
        _animations = new Queue<CommonAnimationState>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _movementState = new MovementState();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (LockMovement) return;

        if (_rigidBody.gravityScale != 1)
        {
            isGravityFlipped = true;
            transform.eulerAngles = transform.eulerAngles = new Vector3(0, 0, 180f);
            movementDirection = -Input.GetAxisRaw("Horizontal");
        }
        else
        {
            isGravityFlipped = false;
            transform.eulerAngles = Vector3.zero;
            movementDirection = Input.GetAxisRaw("Horizontal");
        }
        
        if (IsDead)
        {
            movementDirection = 0f;
            UpdateAnimationState();
            _movements.Enqueue(new MovementPair
            {
                Position = _rigidBody.position,
                HasReceivedInput = false
            });
            return;
        }

        var yVelocity = _rigidBody.velocity.y;

        if (Input.GetKey("space") && IsGrounded())
        {
            if(!isGravityFlipped)
            {
                yVelocity = _jumpHeight;
            }
            else
            {
                yVelocity = -_jumpHeight;
            }
        }

        UpdateAnimationState();

        _movements.Enqueue(new MovementPair
        {
            Position = _rigidBody.position,
            HasReceivedInput = movementDirection != 0f || yVelocity != _rigidBody.velocity.y
        });
        _rigidBody.velocity = new Vector2(movementDirection * _movementSpeed, yVelocity);
    }

    private void UpdateAnimationState()
    {
        CommonAnimationState animationState = new CommonAnimationState
        {
            IsMovingRight = movementDirection > 0f,
            IsMovingLeft = movementDirection < 0f,
            IsJumping = _rigidBody.velocity.y > .1f,
            IsFalling = _rigidBody.velocity.y < -.1f,
            IsGravityFlipped = isGravityFlipped,
        };

        if (movementDirection > 0f)
        {
            _movementState = MovementState.running;
            if (!isGravityFlipped)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }
        else if (movementDirection < 0f)
        {
            _movementState = MovementState.running;
            if (!isGravityFlipped)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            _movementState = MovementState.idle;
        }

        if (_rigidBody.velocity.y > .1f)
        {
            if(!isGravityFlipped)
            {
                _movementState = MovementState.jumping;  
            }
            else
            {
                _movementState = MovementState.falling;
            }
            
        }
        else if (_rigidBody.velocity.y < -.1f)
        {
            if(!isGravityFlipped)
            {
                _movementState = MovementState.falling;  
            }
            else
            {
                _movementState = MovementState.jumping;
            }
        }

        _animator.SetInteger("state", (int)_movementState);

        _animations.Enqueue(animationState);
    }

    private bool IsGrounded()
    {
        if (!isGravityFlipped)
        {
            return Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
        }
        else
        {
            return Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.up, .1f, _jumpableGround);
        }
    }

    public void ResetMovement()
    {
        IsDead = false;
        _movements.Clear();
    }

    public void ResetAnimation()
    {
        _animations.Clear();
    }
}