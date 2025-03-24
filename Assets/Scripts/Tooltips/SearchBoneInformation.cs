using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class SearchBoneInformation : MonoBehaviour
{
    private CallAssistant _callAssistant;
    private string _boneName;

    private void Start()
    {
        _callAssistant = GameObject.Find("AI Manager").GetComponent<CallAssistant>();
        
        Match match = Regex.Match(gameObject.name, @"\([a-zA-Z0-9Á-ýá-ú ]+\)");
        if (match.Success)
        {
            _boneName = match.Value.ReplaceMultiple(new HashSet<char> { '(', ')' }, ' ').Trim();
        }
    }

    public void Search()
    {
        _callAssistant.SetGettingBoneInfo(_boneName);
    }
}
