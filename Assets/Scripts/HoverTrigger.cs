using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{
    [SerializeField]
    private BuildingAreaLogicController buildingAreaLogicController;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    [ContextMenu("Hover")]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        buildingAreaLogicController.enableHover(collision);
    }
}
