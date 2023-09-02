using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuPrefab;

    public static bool IsPaused { get; set; }

    private void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !IsPaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            IsPaused = true;
            Instantiate(_pauseMenuPrefab);
        }
    }
}
