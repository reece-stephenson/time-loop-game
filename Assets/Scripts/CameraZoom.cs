using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float _scrollSpeed = 2f;


    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        var change = _camera.orthographicSize - scroll * _scrollSpeed;

        if (change <= 5 || change >= 15)
            return;

        _camera.orthographicSize = change;
    }
}
