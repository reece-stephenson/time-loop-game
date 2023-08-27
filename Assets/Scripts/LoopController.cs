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
    private List<PlayerCopy> _playerCopies;

    private List<GameObject> _copyPlayers;

    private bool LockReset = false;

    [SerializeField] Color[] loopColours;
    private int currentColorIndex = 0;

    void Start()
    {
        _startPosition = _player.GetComponent<PlayerMovement>().RigidBody.position;
        _copyPlayerColliders = new List<Collider2D>();
        _playerCopies = new List<PlayerCopy>();
        _copyPlayers = new List<GameObject>();
    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime >= timeBetweenClones && !LockReset)
        {
            ResetAll();
        }
    }

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.R) && elapsedTime > 3f)
        {
            if (!LockReset)
                ResetAll();
        }
    }

    private void ResetAll()
    {
        LockReset = true;

        var playerScript = _player.GetComponent<PlayerMovement>();

        playerScript.LockMovement = true;
        playerScript.RigidBody.position = _startPosition;

        var newCopy = Instantiate(_playerCopyPrefab, _startPosition, Quaternion.identity);
        var playerCopyScript = newCopy.GetComponent<PlayerCopy>();
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), newCopy.GetComponent<Collider2D>());

        foreach (var cp in _copyPlayers)
        {
            var collider = cp.GetComponent<Collider2D>();
            var script = cp.GetComponent<PlayerCopy>();


            Physics2D.IgnoreCollision(collider, newCopy.GetComponent<Collider2D>());
            script.ResetMovement(_startPosition);
        }

        _copyPlayers.Add(newCopy);

        if (currentColorIndex == loopColours.Length - 1)
        {
            currentColorIndex = 0;
        }

        playerScript._spriteRenderer.color = loopColours[currentColorIndex + 1];

        Color copySpriteColor = loopColours[currentColorIndex];
        copySpriteColor.a = 0.3f;

        playerCopyScript._spriteRenderer.color = copySpriteColor;

        currentColorIndex++;       

        playerCopyScript.startPosition = _startPosition;
        playerCopyScript._movements = GetDeepCopy(playerScript._movements);

        playerCopyScript._animations = GetAnimationDeepCopy(playerScript._animations);

        foreach (var obj in _resetObjects)
        {
            obj.ResetPosition();
        }

        playerScript.ResetMovement();
        playerScript.ResetAnimation();
        _playerRigidBody.gravityScale = 1;
        playerScript.LockMovement = false;

        elapsedTime = 0f;

        LockReset = false;
    }

    private Queue<MovementPair> GetDeepCopy(Queue<MovementPair> queue)
    {
        var arr = queue.ToArray();
        var ret = new Queue<MovementPair>();

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

public class MovementPair
{
    public Vector2 Position { get; set; }
    public bool HasReceivedInput { get; set; }
}