using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1SceneStart : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _hasPlayed;

    [SerializeField]
    private GameObject _howToPlayPrefab;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private int _levelScene;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_levelScene));
        _audioSource.Play();
    }

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.H) && _player.GetComponent<PlayerMovement>().LockMovement == false && !HowToPlay.LockInstantiation)
        {
            Debug.Log("Created help");
            HowToPlay.LockInstantiation = true;
            Instantiate(_howToPlayPrefab);
        }

    }
}
