using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildingAreaLogicController : MonoBehaviour
{
    [SerializeField]
    private Vector2 _elevatorStartPosition;

    [SerializeField]
    private Vector2 _elevatorEndPosition;

    [SerializeField]
    private float _elevatorMoveSpeed = 2f;

    private Vector2 _elevatorTarget;

    [SerializeField]
    private GameObject _elevatorRigidbody;

    [SerializeField]
    private GameObject _hoverArea;


    [SerializeField]
    private float _hoverAmplitude = 0.7f;

    [SerializeField]
    private float _hoverSpeed = 1f;

    void Start()
    {
        _elevatorTarget = _elevatorStartPosition;

    }

    void Update()
    {
        _elevatorRigidbody.transform.position = Vector2.MoveTowards(_elevatorRigidbody.transform.position, _elevatorTarget, _elevatorMoveSpeed * Time.deltaTime);

    }

    public void elevatorSwitchHit(Collision2D collision)
    {

        if (_elevatorRigidbody.transform.position.y == _elevatorStartPosition.y && _elevatorRigidbody.transform.position.x == _elevatorStartPosition.x)
        {
            _elevatorTarget = _elevatorEndPosition;
        }
        else if (_elevatorRigidbody.transform.position.y == _elevatorEndPosition.y && _elevatorRigidbody.transform.position.x == _elevatorEndPosition.x)
        {
            _elevatorTarget = _elevatorStartPosition;

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
        _elevatorTarget = _elevatorStartPosition;

    }
}
