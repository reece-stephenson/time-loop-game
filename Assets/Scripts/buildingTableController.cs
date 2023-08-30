using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingTableController : MonoBehaviour
{
    [SerializeField]
    private BuildingAreaLogicController buildingAreaLogicController;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string _tag = collision.gameObject.tag;
        if (!_tag.Equals("Untagged"))
        {
            GameObject part = GameObject.FindGameObjectWithTag(_tag).GetComponent<GameObject>();

            collision.gameObject.tag = "Untagged";

        }

    }
}
