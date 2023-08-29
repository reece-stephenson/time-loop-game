using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildingElevatorController : MonoBehaviour
{
    [SerializeField]
    private BuildingAreaLogicController buildingAreaLogicController;

    [SerializeField]
    private int _elevatorToUse;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _audioSource.Play();

        if(_elevatorToUse == 1)
        {
           buildingAreaLogicController.elevatorSwitchHit(1);

        }
        else if (_elevatorToUse == 2)
        {
           buildingAreaLogicController.elevatorSwitchHit(2);

        }

    }

}
