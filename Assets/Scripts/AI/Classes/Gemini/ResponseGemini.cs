using System;
using System.Collections.Generic;

namespace AI.Classes.GeminiResponse
{
    [Serializable]
    public class ResponseGemini
    {
        public Candidate[] candidates;
        public string finishReason;
        public float avgLogprobs;
    }
    
    [Serializable]
    public class Candidate
    {
        public Content content;
    }

    [Serializable]
    public class Content
    {
        public List<Part> parts;
    }
    
    [Serializable]
    public class Part
    {
        public string text;
    }
}