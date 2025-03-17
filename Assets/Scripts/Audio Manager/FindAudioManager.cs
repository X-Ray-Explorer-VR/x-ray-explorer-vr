using Unity.XR.CoreUtils;
using UnityEngine;

public class FindAudioManager : MonoBehaviour
{
    [Header("Properties")]
    public string UIChannelName = "UI";
    private GameObject audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("Audio Manager");
    }

    public void CallOneShotUI(AudioClip audio)
    {
        if (audioManager)
        {
            GameObject channelObject = audioManager.gameObject.GetNamedChild(UIChannelName);
            AudioSource source = channelObject.GetComponent<AudioSource>();

            if (source)
            {
                source.PlayOneShot(audio);
            }
            else
            {
                Debug.LogWarning("Audio Source channel not found");
            }
        }
        else
        {
            Debug.LogWarning("Audio Manager not found");
        }
    }
}
