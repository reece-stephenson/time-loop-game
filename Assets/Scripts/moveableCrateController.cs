using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class moveableCrateController : MonoBehaviour
{

    private Rigidbody2D _rigidbody;

    private int _numCollisions = 0;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _numCollisions = 0;
    }

 
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 6)
        {
            _numCollisions++;
            Debug.Log(_numCollisions);

            if (_numCollisions > 1 )
            {
                _rigidbody.AddForce(new Vector2(800, 0),ForceMode2D.Impulse);
                Debug.Log("FORCE");

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(_numCollisions > 0)
        {
            _numCollisions--;
        }

    }
}
