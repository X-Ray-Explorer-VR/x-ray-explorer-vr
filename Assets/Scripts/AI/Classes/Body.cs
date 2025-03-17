using System;
using System.Collections.Generic;

namespace AI.Classes
{
    [Serializable]
    public class Body
    {
        public string model;
        public int max_tokens;
        public List<Message> messages;
    }
}