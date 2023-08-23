using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public Rigidbody2D RigidBody { get => _rigidBody; }

    [SerializeField]
    float _movementSpeed = 4f;

    [SerializeField]
    float _jumpHeight = 5f;

    public bool LockMovement { get; set; } = false;

    public Queue<Vector2> _movements { get; set; }

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _movements = new Queue<Vector2>();
    }

    void Update()
    {
        if (LockMovement) return;

        var movementDirection = Input.GetAxisRaw("Horizontal");

        var yVelocity = _rigidBody.velocity.y;

        if (Input.GetKey("space"))
            yVelocity = _jumpHeight;

        _movements.Enqueue(_rigidBody.position);
        _rigidBody.velocity = new Vector2(movementDirection * _movementSpeed, yVelocity);
    }

    public void ResetMovement()
    {
        _movements.Clear();
    }
}
