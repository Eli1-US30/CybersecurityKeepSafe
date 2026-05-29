using System;
using System.Collections.Generic;
using System.Text;

namespace CybersecurityChatbot
{
    public class ChatBot
    {
        private KeywordResponder _keywords;
        private SentimentDetector _sentiment;
        private MemoryStore _memory;
        private bool _awaitingName = true;

        public ChatBot()
        {
            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _memory = new MemoryStore();
        }

        public string GetGreeting()
        {
            return "Hello! Welcome to the Cybersecurity Awareness Bot!\nWhat is your name?";
        }

        public string ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type something!";

            input = input.Trim();

            // Step 1 — capture name first
            if (_awaitingName)
            {
                _memory.SetName(input);
                _awaitingName = false;
                return $"Nice to meet you, {_memory.GetName()}! I'm here to help you stay safe online.\n\n" +
                       $"You can ask me about:\n" +
                       $"• Passwords\n• Phishing\n• Viruses\n• VPNs\n• Firewalls\n• Cloud\n• Privacy\n\n" +
                       $"Please make sure that your spelling is right. The code is case sensitive\n" +
                       $"Type 'tell me more' to get more info on the last topic.";
            }

            string lowerInput = input.ToLower().Trim();

            // Step 2 — check for follow up
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("explain more"))
            {
                string followUp = _keywords.GetFollowUp();
                if (followUp != "")
                    return $"Here's more on {_memory.GetLastTopic()}, {_memory.GetName()}:\n{followUp}";
                return "Please ask about a topic first!";
            }

            // Step 3 — detect sentiment
            var sentiment = _sentiment.Detect(lowerInput);
            string opener = "";
            if (sentiment != SentimentDetector.Sentiment.Neutral)
                opener = _sentiment.GetOpener(sentiment);

            // Step 4 — check keywords
            string keywordResponse = _keywords.GetResponse(lowerInput);
            if (keywordResponse != "")
            {
                _memory.SetLastTopic(lowerInput);
                return opener + keywordResponse;
            }

            // Step 5 — special phrases
            if (lowerInput.Contains("how are you"))
                return $"I'm running securely, thank you {_memory.GetName()}!";

            if (lowerInput.Contains("thank you") || lowerInput.Contains("thanks"))
                return $"You're welcome, {_memory.GetName()}!";

            if (lowerInput.Contains("what can you do") || lowerInput.Contains("purpose"))
                return "I can help you learn about:\n• Passwords\n• Phishing\n• Viruses\n• VPNs\n• Firewalls\n\nJust ask me about any of these topics!";

            // Step 6 — fallback
            string[] fallbacks = {
            $"Please make sure the spelling is right, {_memory.GetName()}.",
            $"I'm not sure about that, {_memory.GetName()}. Try asking about passwords, phishing, viruses, VPNs or firewalls.",
            $"I didn't quite understand that, {_memory.GetName()}. Can you rephrase?",
            $"Hmm, I don't have info on that yet, {_memory.GetName()}. Try another cybersecurity topic!"
        };

            Random random = new Random();
            return fallbacks[random.Next(fallbacks.Length)];
        }
    }
}
