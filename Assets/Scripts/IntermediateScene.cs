using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermediateScene : MonoBehaviour
{
    [SerializeField]
    private float _duration = 3f;

    private float _current = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _current = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _current += Time.deltaTime;

        if (_current >= _duration)
        {
            SceneManager.LoadScene(1);
            SceneManager.LoadScene((int)Scenes.SHIP_SCENE, LoadSceneMode.Additive);
            SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
        }
    }
}
