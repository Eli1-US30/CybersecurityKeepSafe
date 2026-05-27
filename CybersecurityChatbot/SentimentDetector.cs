using System;
using System.Collections.Generic;
using System.Text;

namespace CybersecurityChatbot
{
    public class SentimentDetector
    {
        public enum Sentiment
        {
            Neutral,
            Worried,
            Curious,
            Frustrated
        }

        private Dictionary<Sentiment, List<string>> _triggers = new Dictionary<Sentiment, List<string>>
    {
        { Sentiment.Worried, new List<string> {
            "worried", "scared", "afraid", "nervous", "anxious",
            "concerned", "fear", "terrified", "panic", "stress"
        }},
        { Sentiment.Curious, new List<string> {
            "curious", "wondering", "interested", "want to know",
            "how does", "what is", "can you explain", "tell me about"
        }},
        { Sentiment.Frustrated, new List<string> {
            "frustrated", "annoyed", "angry", "confused", "dont understand",
            "don't understand", "this is hard", "complicated", "difficult"
        }}
    };

        private Dictionary<Sentiment, string> _openers = new Dictionary<Sentiment, string>
    {
        { Sentiment.Worried,    "I understand you're feeling worried. Let me help put your mind at ease. " },
        { Sentiment.Curious,    "Great that you're curious! Learning about cybersecurity is really important. " },
        { Sentiment.Frustrated, "I'm sorry you're feeling frustrated. Let me try to explain this more clearly. " }
    };

        public Sentiment Detect(string input)
        {
            input = input.ToLower();

            foreach (var sentiment in _triggers.Keys)
            {
                foreach (var trigger in _triggers[sentiment])
                {
                    if (input.Contains(trigger))
                        return sentiment;
                }
            }
            return Sentiment.Neutral;
        }

        public string GetOpener(Sentiment sentiment)
        {
            if (_openers.ContainsKey(sentiment))
                return _openers[sentiment];
            return "";
        }
    }
}
