using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{

    private float _startingXPos;
    private float _startingYPos;
    // private float _lengthOfSprite;    //This is the length of the sprites.
    public float AmountOfParallax;  //This is amount of parallax scroll. 
    public Camera MainCamera;   //Reference of the camera.

    // Start is called before the first frame update
    void Start()
    {
        //Getting the starting X position of sprite.
        _startingXPos = transform.position.x;
        _startingYPos = transform.position.y;
        //Getting the length of the sprites.
        // _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = MainCamera.transform.position;
        float Temp = Position.x * (1 - AmountOfParallax);

        float DistanceX = Position.x * AmountOfParallax;
        float DistanceY = Position.y * AmountOfParallax;

        Vector3 NewPosition = new Vector3(_startingXPos + DistanceX, _startingYPos + DistanceY, transform.position.z);
        transform.position = NewPosition;
    }
}
