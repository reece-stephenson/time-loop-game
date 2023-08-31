using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public Vector2 startPosition { get; set; }

    public Queue<MovementPair> _movements { get; set; }
    public Queue<CommonAnimationState> _animations { get; set; }

    private IEnumerator _enumerator;
    private IEnumerator _animationEnumerator;

    public SpriteRenderer _spriteRenderer;
    public Animator _animator;

    public MovementState _movementState;

    public float DistanceThreshold { get; set; } = 2f;

    private bool _resetDistance = false;

    public bool IsDead { get; set; }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.position = startPosition;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _movementState = new MovementState();
        _enumerator = _movements.GetEnumerator();
        _enumerator.MoveNext();
        _animationEnumerator = _animations.GetEnumerator();
        _animationEnumerator.MoveNext();
    }

    void FixedUpdate()
    {
        if (!_enumerator.MoveNext() || !_animationEnumerator.MoveNext())
        {
            return;
        }
        else
        {
            if (Vector2.Distance(_rigidBody.position, ((MovementPair)_enumerator.Current).Position) > DistanceThreshold && !_resetDistance)
            {
                return;
            }
        }

        CommonAnimationState animationState = (CommonAnimationState)_animationEnumerator.Current;
        UpdateAnimationState(animationState);

        if (((MovementPair)_enumerator.Current).HasReceivedInput)
            transform.position = Vector2.MoveTowards(_rigidBody.position, ((MovementPair)_enumerator.Current).Position, 4F);

        _resetDistance = false;
    }

    private void UpdateAnimationState(CommonAnimationState animationState)
    {
        if(animationState.IsGravityFlipped)
        {
            _rigidBody.gravityScale = -1;
        }
        else
        {
            _rigidBody.gravityScale = 1;
        }

        if(!animationState.IsGravityFlipped)
        {
            _spriteRenderer.flipY = false;
        }
        else
        {
            _spriteRenderer.flipY = true;
        }

        if (animationState.IsMovingRight)
        {
            _movementState = MovementState.running;
            _spriteRenderer.flipX = false;
        }
        else if (animationState.IsMovingLeft)
        {
            _movementState = MovementState.running;
            _spriteRenderer.flipX = true;
        }
        else
        {
            _movementState = MovementState.idle;
        }

        if (animationState.IsJumping)
        {
            _movementState = MovementState.jumping;
        }
        else if (animationState.IsFalling)
        {
            _movementState = MovementState.falling;
        }

        _animator.SetInteger("state", (int)_movementState);
    }

    private Queue<Vector2> GetDeepCopy(Queue<Vector2> queue)
    {
        var arr = queue.ToArray();
        var ret = new Queue<Vector2>();

        for (int i = 0; i < arr.Length; i++)
        {
            ret.Enqueue(arr[i]);
        }

        return ret;
    }

    public void ResetMovement(Vector2 startPosition)
    {

        _rigidBody.position = startPosition;
        _resetDistance = true;

        _enumerator.Reset();
        _enumerator.MoveNext();

        _animationEnumerator.Reset();
        _animationEnumerator.MoveNext();
    }

    public void PlayDeathSound()
    {
        return;
    }

    public void KillPlayer()
    {
        IsDead = true;
    }
}
