using System.Collections;
using System.Collections.Generic;
using HuggingFace.API;
using UnityEngine;

public class AskQuestion : MonoBehaviour
{
    public string questionContext = "Sistema esquelético humano";
    
    public void Ask(string text)
    {
        HuggingFaceAPI.TextGeneration(text, Debug.Log, Debug.LogWarning);
    }
}
