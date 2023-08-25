using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour
{
    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
    }
}
