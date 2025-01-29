using UnityEngine;
using NativeWebSocket;

public class WebSocketManager : MonoBehaviour
{
    WebSocket websocket;

    private async void Start()
    {
        // Prevent destroing object when changing scenes
        DontDestroyOnLoad(this);

        // Open a new websocket client
        websocket = new WebSocket("ws://localhost:2567");

        websocket.OnOpen += () =>
        {
            Debug.Log("Native WebSockets: Connection open!");
        };

        websocket.OnError += (error) =>
        {
            Debug.LogWarning("Native WebSockets: Error!: " + error);
        };

        websocket.OnClose += (_) =>
        {
            Debug.Log("Native WebSockets: Connection closed!");
        };

        // Wait for messages
        await websocket.Connect();
    }

    private void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
        #endif
    }

    private async void OnApplicationQuit()
    {
        // Close the web socket connection when exiting the app
        await websocket.Close();
    }

    private async void SendWebSocketMessage(string message)
    {
        Debug.Log(websocket.State);

        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
    }

    [ContextMenu("Send test message")]
    public void SendTestMessage()
    {
        SendWebSocketMessage("This is a test message from the Unity client!");
    }
}
