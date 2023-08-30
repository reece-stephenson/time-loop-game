using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField]
    private int _unloadScene;

    [SerializeField]
    private int _loadScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.UnloadSceneAsync(_unloadScene);
        SceneManager.LoadScene(_loadScene, LoadSceneMode.Additive);
    }
}
