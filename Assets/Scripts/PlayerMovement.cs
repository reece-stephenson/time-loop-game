using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public Rigidbody2D RigidBody { get => _rigidBody; }

    private float movementDirection = 0f;

    [SerializeField]
    float _movementSpeed = 4f;

    [SerializeField]
    float _jumpHeight = 5f;

    public bool LockMovement { get; set; } = false;

    public Queue<Vector2> _movements { get; set; }
    public Queue<CommonAnimationState> _animations { get; set; }

    public MovementState _movementState;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _movements = new Queue<Vector2>();
        _animations = new Queue<CommonAnimationState>();
        _movementState = new MovementState();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (LockMovement) return;

        movementDirection = Input.GetAxisRaw("Horizontal");

        var yVelocity = _rigidBody.velocity.y;

        if (Input.GetKey("space"))
        {
            yVelocity = _jumpHeight;
        }

        UpdateAnimationState();

        _movements.Enqueue(_rigidBody.position);
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
        };

        if (movementDirection > 0f)
        {
            _movementState = MovementState.running;
            _spriteRenderer.flipX = false;
        }
        else if (movementDirection < 0f)
        {
            _movementState = MovementState.running;
            _spriteRenderer.flipX = true;
        }
        else
        {
            _movementState = MovementState.idle;
        }

        if (_rigidBody.velocity.y > 0f)
        {
            _movementState = MovementState.jumping;
        }
        else if (_rigidBody.velocity.y < -0f)
        {
            _movementState = MovementState.falling;
        }

        _animator.SetInteger("state", (int)_movementState);

        _animations.Enqueue(animationState);
    }

    public void ResetMovement()
    {
        _movements.Clear();
    }
}