using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildingAreaLogicController : MonoBehaviour
{
    [SerializeField]
    private Vector2 _elevatorStartPosition1;

    [SerializeField]
    private Vector2 _elevatorEndPosition1;

    [SerializeField]
    private Vector2 _elevatorStartPosition2;

    [SerializeField]
    private Vector2 _elevatorEndPosition2;

    [SerializeField]
    private float _elevatorMoveSpeed = 20f;

    private Vector2 _elevatorTarget1;
    private Vector2 _elevatorTarget2;

    [SerializeField]
    private float _hoverAmplitude = 0.7f;

    [SerializeField]
    private float _hoverSpeed = 1f;

    [SerializeField]
    private GameObject _elevator1;

    [SerializeField]
    private GameObject _elevator2;

    [SerializeField]
    private GameObject[] _removePickups;

    void Start()
    {
        _elevatorTarget1 = _elevatorStartPosition1;
        _elevatorTarget2 = _elevatorStartPosition2;

    }

    //void Update()
    //{
    //    //_elevator1.transform.position = Vector2.MoveTowards(_elevator1.transform.position, _elevatorTarget1, _elevatorMoveSpeed * Time.deltaTime);
    //    //_elevator2.transform.position = Vector2.MoveTowards(_elevator2.transform.position, _elevatorTarget2, _elevatorMoveSpeed * Time.deltaTime);

    //}

    public void elevatorSwitchHit(int _elevatorChoice)
    {
        if(_elevatorChoice == 1)
        {
            Debug.Log("here A");
            if (_elevator1.transform.position.y == _elevatorStartPosition1.y && _elevator1.transform.position.x == _elevatorStartPosition1.x)
            {
                Debug.Log("here B");
                _elevatorTarget1 = _elevatorEndPosition1;
            }
            else if (_elevator1.transform.position.y == _elevatorEndPosition1.y && _elevator1.transform.position.x == _elevatorEndPosition1.x)
            {
                Debug.Log("here C");
                _elevatorTarget1 = _elevatorStartPosition1;
            }
        }
        else
        {
            if (_elevator2.transform.position.y == _elevatorStartPosition2.y && _elevator2.transform.position.x == _elevatorStartPosition2.x)
            {

                _elevatorTarget2 = _elevatorEndPosition2;

            }
            else if (_elevator2.transform.position.y == _elevatorEndPosition2.y && _elevator2.transform.position.x == _elevatorEndPosition2.x)
            {
                _elevatorTarget2 = _elevatorStartPosition2;

            }

        }



    }

    public void enableHover(Collision2D collision)
    {
        Vector2 currentPosition = collision.transform.position;
        currentPosition.y = _hoverAmplitude * Mathf.Cos(Time.time * _hoverSpeed);
        collision.transform.position = currentPosition;
    }

    public void ResetObjects()
    {
        _elevatorTarget1 = _elevatorStartPosition1;

        _elevatorTarget2 = _elevatorStartPosition2;
        foreach(var obj in _removePickups)
        {
            obj.GetComponent<pickupObjectController>().resetPickup();
        }

    }
}
