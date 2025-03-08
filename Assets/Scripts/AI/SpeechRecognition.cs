using System.IO;
using HuggingFace.API;
using UnityEngine;

public class SpeechRecognition : MonoBehaviour
{
    private AudioClip clip;
    private bool recording;
    private string microphoneName;

    private AskQuestion askQuestion;

    private void Start()
    {
        askQuestion = GetComponent<AskQuestion>();
        microphoneName = Microphone.devices[2];
    }

    private void Update()
    {
        if (recording && Microphone.GetPosition(microphoneName) >= clip.samples)
        {
            StopRecording();
        }
    }

    [ContextMenu("Start recording")]
    private void StartRecording()
    {
        // TODO: Set proper microphone in runtime
        clip = Microphone.Start(microphoneName, false, 10, 44100);
        recording = true;
    }

    [ContextMenu("Stop recording")]
    private void StopRecording()
    {
        var position = Microphone.GetPosition(microphoneName);
        Microphone.End(microphoneName);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        
        byte[] bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        
        recording = false;
        
        // Send to hugging face speech API
        SendRecording(bytes);
    }

    private void SendRecording(byte[] bytes)
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => askQuestion.Ask(response), Debug.LogWarning);
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }

            return memoryStream.ToArray();
        }
    }
}
