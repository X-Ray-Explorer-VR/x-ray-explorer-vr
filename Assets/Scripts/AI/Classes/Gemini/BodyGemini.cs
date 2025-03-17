using System;
using System.Collections.Generic;

namespace AI.Classes.GeminiBody
{
    [Serializable]
    public class BodyGemini
    {
        public SystemInstruction system_instruction;
        public List<Content> contents;
    }
    
    [Serializable]
    public class SystemInstruction
    {
        public Part parts;
    }
    
    [Serializable]
    public class Part
    {
        public string text;
    }
    
    [Serializable]
    public class Content
    {
        public Part parts;
    }
}