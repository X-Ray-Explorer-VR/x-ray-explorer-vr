using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SetAssistant : MonoBehaviour
{
    [Header("Properties")]
    public bool showingResult;
    [Header("Texts")]
    [SerializeField]
    private string initialMessage = "Presiona X para realizar una pregunta";
    [SerializeField]
    private string listeningMessage = "Escuchando...";
    [SerializeField]
    private string questionMessage = "Preguntaste: \"$\"\n\n<b>Esperando respuesta...</b>";
    [SerializeField]
    private string boneInformationMessage = "Buscando información de $...";
    [Header("References")]
    [SerializeField]
    private GameObject askObject;
    [SerializeField]
    private TextMeshProUGUI askText;
    [SerializeField]
    private GameObject resultObject;
    [SerializeField]
    private TextMeshProUGUI resultTitle;
    [SerializeField]
    private TextMeshProUGUI resultText;
    
    private ToggleTextRelatedTooltips _relatedTooltips;
    
    private void Start()
    {
        showingResult = false;
        _relatedTooltips = GetComponent<ToggleTextRelatedTooltips>();
    }
    
    public void ShowResponse(string questionText, string responseText)
    {
        // Update to a readable text
        if (Regex.IsMatch(responseText, @"/\*\*[A-Za-zÁ-ýá-ú]+\*\*/g"))
        {
            Regex.Replace(responseText, @"/\*\*[A-Za-zÁ-ýá-ú]+\*\*/g", match => $"<b>{match.Value.Replace("**", "")}</b>");
        }
        
        askObject.SetActive(false);
        // Set values
        resultTitle.text = questionText;
        resultText.text = responseText;
        resultObject.SetActive(true);
        // Show related tooltips
        _relatedTooltips.ShowRelatedTooltips(responseText);

        showingResult = true;
    }
    
    public void ShowListening()
    {
        askText.text = listeningMessage;
    }

    public void ShowWaiting(string question)
    {
        askText.text = questionMessage.Replace("$", question);
    }

    public void ShowGettingBoneInfo(string boneName)
    {
        askText.text = boneInformationMessage.Replace("$", boneName);
    }

    public void ResetUI()
    {
        askText.text = initialMessage;
        askObject.SetActive(true);
        
        resultObject.SetActive(false);
        // Hide related tooltips
        _relatedTooltips.HideAllTooltips();

        showingResult = false;
    }
}
