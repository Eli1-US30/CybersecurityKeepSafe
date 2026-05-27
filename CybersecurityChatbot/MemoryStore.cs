using System;
using System.Collections.Generic;
using System.Text;

namespace CybersecurityChatbot
{
    public class MemoryStore
    {
        public string UserName { get; private set; } = "";
        public string LastTopic { get; set; } = "";

        public void SetName(string name)
        {
            UserName = name;
        }

        public string GetName()
        {
            return UserName;
        }

        public bool HasName()
        {
            return !string.IsNullOrWhiteSpace(UserName);
        }

        public void SetLastTopic(string topic)
        {
            LastTopic = topic;
        }

        public string GetLastTopic()
        {
            return LastTopic;
        }
    }
}
