using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCopy : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public Vector2 startPosition { get; set; }

    public Queue<Vector2> _movements { get; set; }
    public Queue<CommonAnimationState> _animations { get; set; }
    public Queue<Vector2> _movementsOriginal { get; set; }

    private IEnumerator _enumerator;
    private IEnumerator _animationEnumerator;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public MovementState _movementState;

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
        //if (_movements != null)
        //    if (_movements.Count > 0)
        //    {
        //        transform.position = Vector2.MoveTowards(_rigidBody.position, _movements.Dequeue(), 1000f);
        //    }
        //    else
        //    {
        //        _rigidBody.position = startPosition;
        //        _movements = GetDeepCopy(_movementsOriginal);
        //    }

        CommonAnimationState animationState = (CommonAnimationState)_animationEnumerator.Current;
        UpdateAnimationState(animationState);

        transform.position = Vector2.MoveTowards(_rigidBody.position, (Vector2)_enumerator.Current, 10000f);


        if (!_enumerator.MoveNext())
        {
            _enumerator.Reset();
            _enumerator.MoveNext();
        }

        if (!_animationEnumerator.MoveNext())
        {
            _animationEnumerator.Reset();
            _animationEnumerator.MoveNext();
        }
    }

    private void UpdateAnimationState(CommonAnimationState animationState)
    {
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
}
