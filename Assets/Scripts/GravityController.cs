using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GravityController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        UpdateGravity();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _rigidbody.gravityScale = 1;
    }

    private void UpdateGravity()
    {
        if (_rigidbody)
        {
            if (_rigidbody.gravityScale == 1)
            {
                _rigidbody.gravityScale = -1;
            }
            else
            {
                _rigidbody.gravityScale = 1;
            }
        }
    }
}
