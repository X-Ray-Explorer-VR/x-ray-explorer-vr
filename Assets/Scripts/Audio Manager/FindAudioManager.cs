using Unity.XR.CoreUtils;
using UnityEngine;

public class FindAudioManager : MonoBehaviour
{
    [Header("Properties")]
    public string UIChannelName = "UI Click";
    
    private GameObject _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio Manager");
    }

    public void CallOneShotUI(AudioClip audio)
    {
        if (_audioManager)
        {
            GameObject channelObject = _audioManager.gameObject.GetNamedChild(UIChannelName);
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
