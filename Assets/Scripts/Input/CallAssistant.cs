using Input.Classes;
using UnityEngine;
using UnityEngine.InputSystem;

public class CallAssistant : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AIEngine engine;
    [SerializeField]
    private InputActionReference triggerAssistant;
    [SerializeField]
    private CanvasGroup assistantCanvas;

    private VoskSpeechToText voskSpeechToText;
    private AskDeepsek askDeepseek;
    private AskGeminiFlash askGeminiFlash;
    private SetAssistant setAssistant;
    private bool searchNextPhrase;
    private bool ready;

    private void Start()
    {
        searchNextPhrase = false;
        ready = true;
        
        voskSpeechToText = GetComponent<VoskSpeechToText>();
        askDeepseek = GetComponent<AskDeepsek>();
        askGeminiFlash = GetComponent<AskGeminiFlash>();
        setAssistant = GetComponent<SetAssistant>();
        voskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        
        triggerAssistant.action.started += OnButtonPressed;
    }

    [ContextMenu("Button Pressed")]
    private void Test()
    {
        if (ready)
        {
            // Update visuals
            setAssistant.ShowListening();
                
            searchNextPhrase = true;
            ready = false;
        }
        else if (setAssistant.showingResult)
        {
            setAssistant.ResetUI();
            ready = true;
        }
    }
    
    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (assistantCanvas.alpha > 0.5 && ready)
        {
            // Update visuals
            setAssistant.ShowListening();
                
            searchNextPhrase = true;
            ready = false;
        }
        else if (setAssistant.showingResult)
        {
            setAssistant.ResetUI();
            ready = true;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnTranscriptionResult(string text)
    {
        if (searchNextPhrase)
        {
            TranscriptionResult result = JsonUtility.FromJson<TranscriptionResult>(text);
            string bestAlternativeText = result.alternatives[0].text.Trim();

            if (!string.IsNullOrWhiteSpace(bestAlternativeText))
            {
                Debug.Log(bestAlternativeText);
                // Update UI
                setAssistant.ShowWaiting(bestAlternativeText);

                switch (engine)
                {
                    default:
                    case AIEngine.Deepseek:
                        Debug.Log("Asking using Deepseek AI");
                        askDeepseek.AskAI(bestAlternativeText);
                        break;
                    case AIEngine.GeminiFlash:
                        Debug.Log("Asking using Gemini Flash AI");
                        askGeminiFlash.AskAI(bestAlternativeText);
                        break;
                }
            }
            else
            {
                Debug.Log("Empty Vosk transcription");
                
                setAssistant.ResetUI();
                
                ready = true;
            }
            
            searchNextPhrase = false;
        }
    }

    public void ChangeAIEngine(AIEngine newEngine)
    {
        engine = newEngine;
    }
}
