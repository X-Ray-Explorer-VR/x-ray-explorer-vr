using System;
using UnityEngine;
using NativeWebSocket;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WebSocketManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private XRSocketInteractor socketInteractor;
    
    private WebSocket _websocket;

    private async void Start()
    {
        // Assign events
        socketInteractor.selectEntered.AddListener((args) =>
        {
            BoneCode bone = args.interactableObject.transform.GetComponent<BoneCode>();
            
            SendWebSocketMessage($"Oculus client: Set {bone.code}");
        });
        socketInteractor.selectExited.AddListener((_) =>
        {
            SendWebSocketMessage("Oculus client: Remove");
        });
        
        // Open a new websocket client
        _websocket = new WebSocket("ws://localhost:8080");

        _websocket.OnOpen += () =>
        {
            Debug.Log("Native WebSockets: Connected");
            // Set initial message
            SendWebSocketMessage("Oculus client: Connected");
        };

        _websocket.OnError += (error) =>
        {
            Debug.LogWarning("Native WebSockets: " + error);
        };

        _websocket.OnClose += (_) =>
        {
            Debug.Log("Native WebSockets: Disconnected");
        };

        // Wait for messages
        await _websocket.Connect();
    }

    private void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            _websocket.DispatchMessageQueue();
        #endif
    }

    private async void OnApplicationQuit()
    {
        try
        {
            // Close the web socket connection when exiting the app
            await _websocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    private async void SendWebSocketMessage(string message)
    {
        try
        {
            if (_websocket.State == WebSocketState.Open)
            {
                await _websocket.SendText(message);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error while sending socket message: {e.Message}");
        }
    }

    [ContextMenu("Send test message")]
    public void SendTestMessage()
    {
        SendWebSocketMessage("This is a test message from the Unity client!");
    }
}
