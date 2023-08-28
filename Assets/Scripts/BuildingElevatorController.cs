using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildingElevatorController : MonoBehaviour
{
    [SerializeField]
    private BuildingAreaLogicController buildingAreaLogicController;

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
        buildingAreaLogicController.elevatorSwitchHit(collision);
    }

}
