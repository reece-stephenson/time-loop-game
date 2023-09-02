using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverAreaController : MonoBehaviour
{
    [SerializeField]
    private float _hoverAmplitude = 0.7f;

    [SerializeField]
    private float _hoverSpeed = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            collision.attachedRigidbody.AddForce(Vector2.up * 10f);
        }
    }
}
