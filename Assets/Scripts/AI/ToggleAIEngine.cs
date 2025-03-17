using UnityEngine;
using UnityEngine.UI;

public class ToggleAIEngine : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Toggle geminiToggle;
    [SerializeField]
    private Toggle deepseekToggle;

    private CallAssistant assistant;
    
    private void Start()
    {
        assistant = GetComponent<CallAssistant>();
        
        geminiToggle.onValueChanged.AddListener(toggle =>
        {
            if (toggle) assistant.ChangeAIEngine(AIEngine.GeminiFlash);
        });
        deepseekToggle.onValueChanged.AddListener(toggle =>
        {
            if (toggle) assistant.ChangeAIEngine(AIEngine.Deepseek);
        });
    }

    private void OnApplicationQuit()
    {
        geminiToggle.onValueChanged.RemoveAllListeners();
        deepseekToggle.onValueChanged.RemoveAllListeners();
    }
}
