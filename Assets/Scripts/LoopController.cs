using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LoopController : MonoBehaviour
{
    private bool StopAll = false;

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

    [SerializeField]
    private BuildingAreaLogicController buildingAreaLogicController;

    [SerializeField]
    private float _startDelay = 15f;
    private bool _hasStarted = false;

    public static int CloneCount { get; set; }

    [SerializeField]
    private Vector2 _startPosition = new Vector2(-44.23f, -5.14f);
    public Vector2 StartPosition { get => _startPosition; }

    [SerializeField]
    private float _distanceThreshold;

    private List<Collider2D> _copyPlayerColliders;
    private List<PlayerCopy> _playerCopies;

    private List<GameObject> _copyPlayers;

    private bool LockReset = false;

    private AudioSource _audioSOurceLoopImminent;
    private bool _playingLoopImminent = false;

    [SerializeField]
    private AudioClip _audioClipLoop;

    [SerializeField]
    private AudioClip _audioClipTime;

    [SerializeField]
    private Vector2[] _unpaintTiles;

    [SerializeField]
    private Tilemap _unpaintTilemap;

    [SerializeField]
    private Sprite _openCryoSprite;

    [SerializeField]
    private GameObject _croyPod;

    void Start()
    {
        _copyPlayerColliders = new List<Collider2D>();
        _playerCopies = new List<PlayerCopy>();
        _copyPlayers = new List<GameObject>();
        _audioSOurceLoopImminent = GetComponent<AudioSource>();
        _player.GetComponent<PlayerMovement>().LockMovement = true;
    }

    void FixedUpdate()
    {
        if (StopAll) return;

        elapsedTime += Time.fixedDeltaTime;

        if (elapsedTime < _startDelay && !_hasStarted)
            return;

        if (!_hasStarted)
        {

            if (_croyPod != null)
            {
                _croyPod.GetComponent<SpriteRenderer>().sprite = _openCryoSprite;
                var pcolor = _player.GetComponent<SpriteRenderer>();
                pcolor.color = new Color(255, 255, 255, 255);
            }

            _player.GetComponent<PlayerMovement>().LockMovement = false;
            _hasStarted = true;
            elapsedTime = 0;
        }

        if (elapsedTime >= timeBetweenClones - 5f && !_playingLoopImminent)
        {
            _playingLoopImminent = true;
            _audioSOurceLoopImminent.clip = _audioClipLoop;
            _audioSOurceLoopImminent.Play();
        }

        if (elapsedTime >= timeBetweenClones && !LockReset)
        {
            ResetAll();
        }

        var playerMovement = _player.GetComponent<PlayerMovement>();
        if (playerMovement.IsDead && !LockReset)
        {
            ResetAll();
        }
    }

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.R) && elapsedTime > 3f && _hasStarted)
        {
            if (!LockReset)
                ResetAll();
        }
    }

    private void ResetAll()
    {
        CloneCount++;
        _playingLoopImminent = false;
        LockReset = true;

        var playerScript = _player.GetComponent<PlayerMovement>();

        playerScript.LockMovement = true;
        playerScript._animator.SetTrigger("loopTrigger");
        playerScript.RigidBody.position = _startPosition;

        var newCopy = Instantiate(_playerCopyPrefab, _startPosition, Quaternion.identity);
        var playerCopyScript = newCopy.GetComponent<PlayerCopy>();
        playerCopyScript.DistanceThreshold = _distanceThreshold;
        Physics2D.IgnoreCollision(_player.GetComponent<CapsuleCollider2D>(), newCopy.GetComponent<CapsuleCollider2D>());

        _player.tag = "Untagged";

        foreach (var cp in _copyPlayers)
        {
            var collider = cp.GetComponent<CapsuleCollider2D>();
            var script = cp.GetComponent<PlayerCopy>();

            cp.tag = "Untagged";

            if (cp.GetComponent<PlayerCopy>().IsDead)
            {
                cp.GetComponent<PlayerCopy>()._animator.SetInteger("state", (int)MovementState.idle);
            }

            Physics2D.IgnoreCollision(collider, newCopy.GetComponent<CapsuleCollider2D>());
            script.ResetMovement(_startPosition);
        }

        _copyPlayers.Add(newCopy);

        Color copySpriteColour = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);;
        copySpriteColour.a = 0.5f;

        playerCopyScript._spriteRenderer.color = copySpriteColour;


        playerCopyScript.startPosition = _startPosition;
        playerCopyScript._movements = GetDeepCopy(playerScript._movements);

        playerCopyScript._animations = GetAnimationDeepCopy(playerScript._animations);

        foreach (var obj in _resetObjects)
        {
            obj.ResetPosition();
        }

        if (buildingAreaLogicController != null)
            buildingAreaLogicController.ResetObjects();
        playerScript.ResetMovement();
        playerScript.ResetAnimation();
        _playerRigidBody.gravityScale = 1;

        if (_unpaintTilemap != null)
        foreach (var unpaintTile in _unpaintTiles)
        {
            _unpaintTilemap.SetTile(new Vector3Int((int)unpaintTile.x, (int)unpaintTile.y), null);
        }


        _audioSOurceLoopImminent.clip = _audioClipTime;
        _audioSOurceLoopImminent.Play();

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