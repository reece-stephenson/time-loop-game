using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAllScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Starting");
        LeaderboardController.Instance.StartAll();
        Debug.Log("Started");
    }
}
