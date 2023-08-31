using UnityEngine;

public class PlayerAudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip jumpSound;

    [SerializeField]
    AudioClip deathSound;
    
    [SerializeField]
    AudioClip loopSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayLoopSound()
    {
        audioSource.PlayOneShot(loopSound);
    }
}
