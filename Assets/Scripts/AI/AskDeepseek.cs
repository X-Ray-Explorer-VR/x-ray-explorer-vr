using System;
using System.Collections;
using System.Collections.Generic;
using AI.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AskDeepsek : MonoBehaviour
{
    [Header("API")]
    [SerializeField]
    private string apiUrl = "https://api.deepseek.com/chat/completions";
    [SerializeField]
    private string apiKey;
    [Header("Request config")]
    [SerializeField]
    private string model = "deepseek-chat";
    [SerializeField]
    private int maxTokens = 128;
    [SerializeField]
    [TextArea]
    private string systemInitialMessage = "Eres un médico especialista en anatomía. Que no sea una respuesta mayor a dos párrafos";

    private SetAssistant setAssistant;

    private void Start()
    {
        setAssistant = GetComponent<SetAssistant>();
    }

    public void AskAI(string text)
    {
        StartCoroutine(SendRequest(text));
    }

    private IEnumerator SendRequest(string questionText)
    {
        List<Message> messages = new List<Message>(2)
        {
            new Message("system", systemInitialMessage),
            new Message("user", questionText)
        };

        Body body = new Body
        {
            model = model,
            max_tokens = maxTokens,
            messages = messages
        };

        string jsonString = JsonUtility.ToJson(body);
        
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, jsonString, "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        
        Debug.Log("Sending message...");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
            string responseText = response.choices[0].message.content;
            
            // Update GUI
            setAssistant.ShowResponse(questionText, responseText);
        }
        
        Debug.Log("Message ended");
    }
}
