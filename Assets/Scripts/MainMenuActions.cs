using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour
{

    public static bool PlayButtonIsEnabled { get; set; }

    public void PlayButtonClick()
    {
        if (!PlayButtonIsEnabled) return;

        SceneManager.LoadScene((int)Scenes.LEVEL1);
        SceneManager.LoadScene((int)Scenes.SHIP_SCENE, LoadSceneMode.Additive);
        SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
    }

    public void HighscoresButtonCLick()
    {
        LeaderboardController.Instance.SubmitScore();
    }

    public async void QuitButtonClick()
    {
        LeaderboardController.Instance.GetScores((response) =>
        {
            if (response.success)
            {
                Debug.Log("fetched");
                Debug.Log(response.items.Length);

                foreach (var item in response.items)
                {
                    Debug.Log(item.member_id);
                }
            }
            else
            {
                Debug.Log("Not fetched");
                Debug.Log(response.Error);
            }
        });

        
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
