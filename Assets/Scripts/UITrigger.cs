using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class UITrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    private GameObject displayedText;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (text != null && other.gameObject.tag == "Player")
        {
            ShowFloatingText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (text != null && other.gameObject.tag == "Player")
        {
            HideFloatingText();
        }
    }

    void ShowFloatingText()
    {

        displayedText = Instantiate(text, transform.position + offset, Quaternion.identity);
    }

    void HideFloatingText()
    {
        Destroy(displayedText);
    }
}
