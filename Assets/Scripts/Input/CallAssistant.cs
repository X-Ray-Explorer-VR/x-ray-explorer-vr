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

    private VoskSpeechToText _voskSpeechToText;
    private AskDeepsek _askDeepseek;
    private AskGeminiFlash _askGeminiFlash;
    private SetAssistant _setAssistant;
    private bool _searchNextPhrase;
    private bool _ready;

    private void Start()
    {
        _searchNextPhrase = false;
        _ready = true;
        
        _voskSpeechToText = GetComponent<VoskSpeechToText>();
        _askDeepseek = GetComponent<AskDeepsek>();
        _askGeminiFlash = GetComponent<AskGeminiFlash>();
        _setAssistant = GetComponent<SetAssistant>();
        _voskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        
        triggerAssistant.action.started += OnButtonPressed;
    }

    [ContextMenu("Button Pressed")]
    private void Test()
    {
        if (_ready)
        {
            // Update visuals
            _setAssistant.ShowListening();
                
            _searchNextPhrase = true;
            _ready = false;
        }
        else if (_setAssistant.showingResult)
        {
            _setAssistant.ResetUI();
            _ready = true;
        }
    }
    
    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (assistantCanvas.alpha > 0.5 && _ready)
        {
            // Update visuals
            _setAssistant.ShowListening();
                
            _searchNextPhrase = true;
            _ready = false;
        }
        else if (_setAssistant.showingResult)
        {
            _setAssistant.ResetUI();
            _ready = true;
        }
    }

    public void SetGettingBoneInfo(string boneName)
    {
        if (string.IsNullOrEmpty(boneName)) return;
        
        // Update UI
        _setAssistant.ShowGettingBoneInfo(boneName);
        
        SendToAI(boneName);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnTranscriptionResult(string text)
    {
        if (!_searchNextPhrase) return;
        
        TranscriptionResult result = JsonUtility.FromJson<TranscriptionResult>(text);
        string bestAlternativeText = result.alternatives[0].text.Trim();

        if (!string.IsNullOrWhiteSpace(bestAlternativeText))
        {
            Debug.Log(bestAlternativeText);
            // Update UI
            _setAssistant.ShowWaiting(bestAlternativeText);
            
            SendToAI(bestAlternativeText);
        }
        else
        {
            Debug.Log("Empty Vosk transcription");
                
            _setAssistant.ResetUI();
                
            _ready = true;
        }
            
        _searchNextPhrase = false;
    }

    private void SendToAI(string text)
    {
        switch (engine)
        {
            default:
            case AIEngine.Deepseek:
                Debug.Log("Asking using Deepseek AI");
                _askDeepseek.AskAI(text);
                break;
            case AIEngine.GeminiFlash:
                Debug.Log("Asking using Gemini Flash AI");
                _askGeminiFlash.AskAI(text);
                break;
        }
    }

    public void ChangeAIEngine(AIEngine newEngine)
    {
        engine = newEngine;
    }
}
