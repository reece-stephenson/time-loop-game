using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class pickupObjectController : MonoBehaviour
{

    private GameObject _player;

    private Vector2 _startPosition;

    private Vector2 _buildingTablePosition;

    [SerializeField]
    private Vector2 _positionToBePlaced;
    private bool _isPlaced { get; set; }
    private bool _isPickedUp { get; set; }

    private static int _placedCount = 0;

    [SerializeField]
    private bool _isMagnet = false;

    [SerializeField]
    private bool _isMagnetic = false;

    [SerializeField]
    private Tilemap _tilemap;

    [SerializeField]
    private Vector2[] _unpaint;

    [SerializeField]
    private Tile[] _paintTile;

    private Quaternion _startRotation;

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;

        _buildingTablePosition = new Vector2(18.8f, -8.5f);

        _isPlaced = false;
        _isPickedUp = false;

    }

    void FixedUpdate()
    {

        Vector2 _currPos = transform.position;
        float _dist = Vector2.Distance(_currPos, _buildingTablePosition);

        if (_dist < 1.9)
        {
            if (!_isPlaced)
                _placedCount++;
            
            transform.rotation = _startRotation;

            transform.position = _positionToBePlaced;
            _isPlaced = true;
            _player.gameObject.tag = "Untagged";

            if (_placedCount == 5 && _tilemap != null)
            {
                Debug.Log("Finished building");
                foreach (var pos in _unpaint)
                {
                    _tilemap.SetTile(new Vector3Int((int)pos.x, (int)pos.y), null);
                }
            }

            return;
        }
        else if (_isPickedUp && !_isPlaced)
        {
            Vector3 _playerPos = _player.transform.position;
            transform.position = new Vector2(_playerPos.x, _playerPos.y - 0.1f);
        }
        else if (!_isPlaced)
        {
            transform.Rotate(0f, 40f * Time.deltaTime, 0f, Space.World);
        }
    }

    public void pickUpObject(Collider2D collision)
    {
        if (_isMagnetic && !collision.gameObject.tag.Equals("CarryingMagnet"))
        {
            return;
        }
        if ( _isPickedUp || collision.gameObject.tag.Equals("CarryingObject"))
        {
            return;
        }

        if (collision.gameObject.name.Equals("Player") || collision.gameObject.name.Equals("PlayerCopy(Clone)"))
        {
            GetComponent<Rigidbody2D>().mass = 0.001f;
            //   transform.position = new Vector2(_startPosition.x + 0.1f, _startPosition.y);


            _isPickedUp = true;
            _player = collision.gameObject;
            transform.rotation = _startRotation;

            if (!_isMagnet)
            {
                _player.tag = "CarryingObject";

            }
            else
            {
                _player.tag = "CarryingMagnet";

            }

        }
    }

    public static bool NextBoolean()
    {
        System.Random random = new System.Random();
        return random.Next() > (Int32.MaxValue / 2);
    }

    public void resetPickup()
    {
        _isPickedUp =false;
        _isPlaced=false;

        _placedCount = 0;
        transform.position = _startPosition;

        if (_tilemap == null) return;
        _tilemap.SetTile(new Vector3Int((int)_unpaint[0].x, (int)_unpaint[0].y), _paintTile[0]);
        _tilemap.SetTile(new Vector3Int((int)_unpaint[1].x, (int)_unpaint[1].y), _paintTile[1]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickUpObject(collision);
    }
}
