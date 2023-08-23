using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    [SerializeField]
    private int _maxFrames = 6000;
    private int _currentFrame = 0;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _playerCopyPrefab;

    private Vector2 _startPosition;
    public Vector2 StartPosition { get => _startPosition; }

    private List<Collider2D> _copyPlayerColliders;

    void Start()
    {
        _startPosition = new Vector2(-9.2f, -2.5f);
        _copyPlayerColliders = new List<Collider2D>();
    }

    void Update()
    {
        if (_currentFrame < _maxFrames)
        {
            _currentFrame++;
        }
        else
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

            playerScript.ResetMovement();
            playerScript.LockMovement = false;

            _currentFrame = 0;

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
                IsMovingLeft = arr[i].IsMovingLeft
            });
        }

        return ret;
    }
}
