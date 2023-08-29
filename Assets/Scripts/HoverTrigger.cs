using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{
    [SerializeField]
    private GameObject hoverArea;

    [SerializeField]
    private GameObject particles;
    void Start()
    {

    }


    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        hoverArea.SetActive(true);
        particles.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        hoverArea.SetActive(false);
        particles.SetActive(false);
    }


}
