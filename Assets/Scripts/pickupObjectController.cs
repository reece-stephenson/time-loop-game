using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickupObjectController : MonoBehaviour
{

    private GameObject _player;

    private Vector2 _startPosition;

    private Vector2 _buildingTablePosition;

    [SerializeField]
    private Vector2 _positionToBePlaced;
    private bool _isPlaced { get; set; }
    private bool _isPickedUp { get; set; }

    [SerializeField]
    private bool _isMagnet = false;

    [SerializeField]
    private bool _isMagnetic = false;

    void Start()
    {
        _startPosition = transform.position;

        _buildingTablePosition = new Vector2(18.8f, -8.5f);

        _isPlaced = false;
        _isPickedUp = false;

    }
    void Update()
    {

        Vector2 _currPos = transform.position;
        float _dist = Vector2.Distance(_currPos, _buildingTablePosition);

        if (_dist < 1.9)
        {
            transform.position = _positionToBePlaced;
            _isPlaced = true;
            _player.gameObject.tag = "Untagged";

            return;
        }

        if (_isPickedUp)
        {
            Vector3 _playerPos = _player.transform.position;
            transform.position = new Vector2(_playerPos.x, _playerPos.y - 0.1f);
        }

        


    }

    public void pickUpObject(Collider2D collision)
    {
        if(_isMagnetic && !collision.gameObject.tag.Equals("CarryingMagnet"))
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

    public void resetPickup()
    {
        _isPickedUp=false;
        _isPlaced=false;

        transform.position = _startPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickUpObject(collision);

    }
}
