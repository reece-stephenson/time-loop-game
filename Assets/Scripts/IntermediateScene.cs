using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntermediateScene : MonoBehaviour
{
    [SerializeField]
    private float _duration = 3f;

    [SerializeField]
    private TMP_Text _text;

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
            _text.text = "Click to continue...";
        }
    }

    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0) && _current >= _duration)
        {
            SceneManager.LoadScene((int)Scenes.MAIN_MENU);
            SceneManager.LoadScene((int)Scenes.DEV_SCENE, LoadSceneMode.Additive);
        }
    }
}
