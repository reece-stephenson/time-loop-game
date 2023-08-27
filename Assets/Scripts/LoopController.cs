using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenClones = 10f;
    private float elapsedTime = 0f;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _playerCopyPrefab;

    [SerializeField]
    private Resetable[] _resetObjects;

    [SerializeField]
    private Rigidbody2D _playerRigidBody;

    private Vector2 _startPosition;
    public Vector2 StartPosition { get => _startPosition; }

    private List<Collider2D> _copyPlayerColliders;

    [SerializeField] Color[] loopColours;
    private int currentColorIndex = 0;

    void Start()
    {
        _startPosition = _player.GetComponent<PlayerMovement>().RigidBody.position;
        _copyPlayerColliders = new List<Collider2D>();
    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime >= timeBetweenClones)
        {
            var playerScript = _player.GetComponent<PlayerMovement>();

            playerScript.LockMovement = true;
            playerScript.RigidBody.position = _startPosition;

            var newCopy = Instantiate(_playerCopyPrefab, _startPosition, Quaternion.identity);

            var playerCopyScript = newCopy.GetComponent<PlayerCopy>();
            playerCopyScript.startPosition = _startPosition;

            Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), newCopy.GetComponent<Collider2D>());

            foreach (var collider in _copyPlayerColliders)
            {
                Physics2D.IgnoreCollision(collider, newCopy.GetComponent<Collider2D>());
            }

            _copyPlayerColliders.Add(newCopy.GetComponent<Collider2D>());

            playerCopyScript.startPosition = _startPosition;
            playerCopyScript._movements = GetDeepCopy(playerScript._movements);

            playerCopyScript._animations = GetAnimationDeepCopy(playerScript._animations);

            if(currentColorIndex == loopColours.Length - 1)
            {
                currentColorIndex = 0;
            }

            playerScript._spriteRenderer.color = loopColours[currentColorIndex + 1];

            Color copySpriteColor = loopColours[currentColorIndex];
            copySpriteColor.a = 0.3f;

            playerCopyScript._spriteRenderer.color = copySpriteColor;

            currentColorIndex++;

            foreach (var obj in _resetObjects)
            {
                obj.ResetPosition();
            }

            playerScript.ResetMovement();
            _playerRigidBody.gravityScale = 1;
            playerScript.ResetAnimation();
            playerScript.LockMovement = false;

            elapsedTime = 0f;

        }
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
    private Queue<CommonAnimationState> GetAnimationDeepCopy(Queue<CommonAnimationState> queue)
    {
        var arr = queue.ToArray();
        var ret = new Queue<CommonAnimationState>();

        for (int i = 0; i < arr.Length; i++)
        {
            ret.Enqueue(new CommonAnimationState
            {
                IsMovingRight = arr[i].IsMovingRight,
                IsMovingLeft = arr[i].IsMovingLeft,
                IsJumping = arr[i].IsJumping,
                IsFalling = arr[i].IsFalling,
                IsGravityFlipped = arr[i].IsGravityFlipped,
            });
        }

        return ret;
    }
}
