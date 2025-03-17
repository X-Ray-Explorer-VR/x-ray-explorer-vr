using System;
using System.Collections.Generic;

namespace AI.Classes
{
    [Serializable]
    public class Response
    {
        public string id;
        public int created;
        public string model;
        public List<Choice> choices;
        public string system_fingerprint;
    }

    [Serializable]
    public class Choice
    {
        public int index;
        public Message message;
        public string finish_reason;
    }
}