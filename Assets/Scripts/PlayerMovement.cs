using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public SpriteRenderer _spriteRenderer;
    public Animator _animator;
    [SerializeField] private LayerMask _jumpableGround;
    public Rigidbody2D RigidBody { get => _rigidBody; }

    public bool IsDead { get; set; }

    private CapsuleCollider2D _capsuleCollider;

    private float movementDirection = 0f;

    [SerializeField]
    float _movementSpeed = 4f;

    [SerializeField]
    float _jumpHeight = 5f;

    [SerializeField]
    private PhysicsMaterial2D _physicsMaterialSlide;

    [SerializeField]
    private PhysicsMaterial2D _physicsMaterialNoSlide;

    private bool _lockMovement;
    public bool LockMovement
    {
        get => _lockMovement;
        set
        {
            _lockMovement = value;
            _capsuleCollider.sharedMaterial = (value) ? _physicsMaterialNoSlide : _physicsMaterialSlide;
        }
    }

    public Queue<MovementPair> _movements { get; set; }
    public Queue<CommonAnimationState> _animations { get; set; }

    public MovementState _movementState;

    private bool top;

    private bool isGravityFlipped;

    private PlayerAudioPlayer _playerAudioPlayer;

    void Start()
    {
        _playerAudioPlayer = GetComponent<PlayerAudioPlayer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _movements = new Queue<MovementPair>();
        _animations = new Queue<CommonAnimationState>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _movementState = new MovementState();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (LockMovement)
        {
            //Debug.Log("Not moving A");
            return;
        }

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
            // Debug.Log("Not moving B");
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

            _playerAudioPlayer.PlayJumpSound();
            if (!isGravityFlipped)
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
            if (!isGravityFlipped)
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
            if (!isGravityFlipped)
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
        float castDistance = 0.1f;
        Vector2 castDirection = !isGravityFlipped ? Vector2.down : Vector2.up;

        Vector2 capsuleCenter = _capsuleCollider.bounds.center;
        float capsuleHeight = _capsuleCollider.bounds.size.y;
        float capsuleRadius = _capsuleCollider.size.x / 2f;

        RaycastHit2D hit = Physics2D.CapsuleCast(capsuleCenter, new Vector2(capsuleRadius * 2, capsuleHeight), CapsuleDirection2D.Vertical, 0f, castDirection, castDistance, _jumpableGround);

        return hit.collider != null;
    }



    public void ResetMovement()
    {
        LockMovement = false;
        IsDead = false;
        _movements.Clear();
    }

    public void ResetAnimation()
    {
        _animations.Clear();
    }

    public void PlayDeathSound()
    {
        _playerAudioPlayer.PlayDeathSound();
    }

    public void PlayLoopSound()
    {
        _playerAudioPlayer.PlayLoopSound();
    }

    public void KillPlayer()
    {
        IsDead = true;
    }
}