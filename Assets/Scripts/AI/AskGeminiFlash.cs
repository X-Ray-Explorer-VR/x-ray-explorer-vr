using System.Collections;
using System.Collections.Generic;
using AI.Classes.GeminiBody;
using AI.Classes.GeminiResponse;
using UnityEngine;
using UnityEngine.Networking;
using Part = AI.Classes.GeminiBody.Part;

public class AskGeminiFlash : MonoBehaviour
{
    [Header("API")]
    [SerializeField]
    private string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";
    [SerializeField]
    private string apiKey;
    [Header("Request config")]
    [SerializeField]
    [TextArea]
    private string systemInitialMessage = "Eres un médico especialista en el esqueleto humano. Que no sea una respuesta mayor a dos párrafos. Sólo puedes contestar una pregunta a la vez. No hagas preguntas.";

    private SetAssistant setAssistant;

    private void Start()
    {
        setAssistant = GetComponent<SetAssistant>();
    }
    
    [ContextMenu("Test API")]
    public void TestAI()
    {
        StartCoroutine(SendRequest("¿Qué huesos componen la pierna?"));
    }

    public void AskAI(string text)
    {
        StartCoroutine(SendRequest(text));
    }

    private IEnumerator SendRequest(string questionText)
    {
        BodyGemini body = new BodyGemini()
        {
            system_instruction = new SystemInstruction()
            {
                parts = new Part()
                {
                    text = systemInitialMessage
                }
            },
            contents = new List<AI.Classes.GeminiBody.Content>()
            {
                new()
                {
                    parts = new Part()
                    {
                        text = questionText
                    }
                }
            }
        };
        
        string jsonString = JsonUtility.ToJson(body);
        
        Debug.Log(jsonString);
        
        UnityWebRequest request = UnityWebRequest.Post($"{apiUrl}?key={apiKey}", jsonString, "application/json");
        
        Debug.Log("Sending message...");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            ResponseGemini response = JsonUtility.FromJson<ResponseGemini>(request.downloadHandler.text);
            string responseText = response.candidates[0].content.parts[0].text;
            
            // Update GUI
            setAssistant.ShowResponse(questionText, responseText);
        }
        
        Debug.Log("Message ended");
    }
}
