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
        if (collision.gameObject.name.Equals("Player") || collision.gameObject.name.Equals("PlayerCopy(Clone)"))
        {
            Vector2 currentPosition = collision.transform.position;
            currentPosition.y = _hoverAmplitude * Mathf.Cos(Time.time * _hoverSpeed);
            collision.transform.position = currentPosition;
        }
    }
}
