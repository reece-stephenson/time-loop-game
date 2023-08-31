using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        LevelRestart.IsPaused = false;
        Destroy(gameObject);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        LevelRestart.IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene((int)Scenes.SHIP_SCENE, LoadSceneMode.Additive);
        SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        LevelRestart.IsPaused = false;
        SceneManager.LoadScene((int)Scenes.MAIN_MENU);
        SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
    }
}
