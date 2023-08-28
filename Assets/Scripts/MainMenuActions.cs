using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{
    public void PlayButtonClick()
    {
        SceneManager.LoadScene((int)Scenes.LEVEL1);
        SceneManager.LoadScene((int)Scenes.SHIP_SCENE, LoadSceneMode.Additive);
        SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
    }
}

public enum Scenes
{
    MAIN_MENU = 0,
    LEVEL1 = 1,
    LEVEL2 = 2,
    LEVEL3 = 3,
    SHIP_SCENE = 4,
    DEV_SCENE = 5
}
