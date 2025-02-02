using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [Header("References")]
    public AudioSource clickAudio;

    public void PlaySocketSound()
    {
        clickAudio.Play();
    }
}
