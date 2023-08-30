using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    private static bool _hasRestarted;

    [SerializeField]
    private int _levelScene;

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene((int)Scenes.SHIP_SCENE, LoadSceneMode.Additive);
        SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
    }

    private void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_hasRestarted)
        {
            _hasRestarted = true;
            Debug.Log("restarting level");
            RestartLevel();
        }
    }
}
