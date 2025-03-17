using System;
using System.Collections.Generic;

namespace Input.Classes
{
    [Serializable]
    public class TranscriptionResult
    {
        public List<Alternative> alternatives;
    }

    [Serializable]
    public class Alternative
    {
        public float confidence;
        public string text;
    }
}