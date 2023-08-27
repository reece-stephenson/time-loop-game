using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    [SerializeField]
    private Vector2 _startPosition;

    [SerializeField]
    private Vector2 _endPosition;

    [SerializeField]
    private GameObject _rigidbody;

    [SerializeField]
    private float _moveSpeed = 2f;

    private int _collisionCount = 0;

    private Vector2 _target;

    private AudioSource _audioSource;

    void Start()
    {
        _target = _startPosition;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _rigidbody.transform.position = Vector2.MoveTowards(_rigidbody.transform.position, _target, _moveSpeed * Time.deltaTime);
    }

    public void Activate()
    {
        _audioSource.Play();
        _target = _endPosition;
    }

    public void Deactivate()
    {
        _audioSource.Play();
        _target = _startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionCount++;
        UpdateElevator();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _collisionCount--;
        UpdateElevator();
    }

    private void UpdateElevator()
    {
        if (_collisionCount % 2 == 0)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }
}
