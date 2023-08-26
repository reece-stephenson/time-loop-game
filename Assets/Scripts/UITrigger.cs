using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject text;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(text != null && other.gameObject.tag == "Player")
        {
            ShowFloatingText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        HideFloatingText();
    }

    void ShowFloatingText()
    {
        Debug.Log(text.GetComponent<TextMesh>().text);
    }

    void HideFloatingText()
    {

    }
}
