using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{

    public static bool PlayButtonIsEnabled { get; set; }

    public static bool HasClickedPlay { get; set; }

    public void PlayButtonClick()
    {
        HasClickedPlay = true;

        SceneManager.LoadScene((int)Scenes.LEVEL1);
        SceneManager.LoadScene((int)Scenes.SHIP_SCENE, LoadSceneMode.Additive);
        SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
    }

    public void HighscoresButtonCLick()
    {

    }

    public void QuitButtonClick()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }

}

public enum Scenes
{
    MAIN_MENU = 7,
    LEVEL1 = 1,
    LEVEL2 = 2,
    LEVEL3 = 3,
    SHIP_SCENE = 4,
    DEV_SCENE = 5,
    FINAL_SCENE = 6
}
