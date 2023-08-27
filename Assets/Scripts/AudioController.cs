using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _backgroundSound;

    void Start()
    {
        _backgroundSound.Play();
    }
}
