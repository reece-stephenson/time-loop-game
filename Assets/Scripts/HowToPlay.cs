using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public static bool LockInstantiation { get; set; }

    private void Start()
    {
        Time.timeScale = 0f;
        LockInstantiation = true;
    }

    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Closing help");
            Time.timeScale = 1f;
            Destroy(gameObject);
            LockInstantiation = false;
        }
    }
}
