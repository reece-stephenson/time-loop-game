using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public static bool LockInstantiation { get; set; }

    public static bool LockPopup { get; set; } = false;

    private void Start()
    {
        Time.timeScale = 0f;
        LockInstantiation = true;
    }

    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0) && !LockPopup)
        {
            Debug.Log("Closing help: " + LockPopup);
            Time.timeScale = 1f;
            Destroy(gameObject);
            LockInstantiation = false;
        }
    }
}
