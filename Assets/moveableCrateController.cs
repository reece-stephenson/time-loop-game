using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveableCrateController : MonoBehaviour
{
    public GameObject moveableCrate;
    private int _collisionCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name.Equals("Player") || collision.gameObject.name.Equals("PlayerCopy(Clone)"))
        {
            _collisionCount++;
            Debug.Log(_collisionCount);
        }

        if (_collisionCount > 1)
        {
            moveableCrate.GetComponent<Rigidbody2D>().mass = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.name.Equals("PlayerCopy(Clone)"))
        {
            _collisionCount--;
        }

        if (_collisionCount < 1)
        {
            moveableCrate.GetComponent<Rigidbody2D>().mass = 100;
        }
    }
}
