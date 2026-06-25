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
        private DatabaseHelper _db = new DatabaseHelper();
        private bool _awaitingTaskTitle = false;
        private bool _awaitingReminderChoice = false;
        private string _pendingTaskTitle = "";
        private QuizGame _quiz = new QuizGame();
        private ActivityLogger _activityLog = new ActivityLogger();

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
                return "Please enter your name!";

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
                       $"Type 'tell me more' to get more info on the last topic you asked about.";
            }

            string lowerInput = input.ToLower().Trim();

            // Handle ongoing task conversation
            if (_awaitingTaskTitle || _awaitingReminderChoice)
                return HandleTaskFlow(input);

            // Detect "add task" command - multiple phrasings
            if (lowerInput.Contains("add task") || lowerInput.Contains("add a task") ||
                lowerInput.Contains("new task") || lowerInput.Contains("create a task") ||
                lowerInput.Contains("remind me"))
            {
                _awaitingTaskTitle = true;
                return "Sure! What is the task title or description?";
            }

            // Detect "show tasks" command - multiple phrasings
            if (lowerInput.Contains("show tasks") || lowerInput.Contains("view tasks") ||
                lowerInput.Contains("my tasks") || lowerInput.Contains("what tasks") ||
                lowerInput.Contains("list tasks") || lowerInput.Contains("what have you done"))
            {
                var tasks = _db.GetAllTasks();
                if (tasks.Count == 0)
                    return "You have no tasks yet!";

                string result = "Here are your tasks:\n";
                foreach (var t in tasks)
                {
                    string status = t.IsCompleted ? "[Done]" : "[Pending]";
                    string reminder = t.ReminderDate.HasValue ? $" - Reminder: {t.ReminderDate.Value.ToShortDateString()}" : "";
                    result += $"{status} #{t.TaskId}: {t.Title}{reminder}\n";
                }
                return result;
            }

            // If quiz is active, treat input as an answer
            if (_quiz.IsActive)
            {
                string quizResponse = _quiz.SubmitAnswer(input);
                if (!_quiz.IsActive) // quiz just finished
                {
                    _activityLog.LogAction($"Quiz completed - scored {_quiz.Score}/{_quiz.TotalQuestions}");
                }
                return quizResponse;
            }

            // Detect "start quiz" command
            if (lowerInput.Contains("quiz") || lowerInput.Contains("start quiz"))
            {
                _activityLog.LogAction("Quiz started - 10 questions");
                return _quiz.Start();
            }

            // Activity log - keep "what have you done" here only
            if (lowerInput.Contains("activity log") || lowerInput.Contains("show log"))
            {
                return _activityLog.GetRecentLog();
            }

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
        private string HandleTaskFlow(string input)
        {
            // Step A — waiting for the task title
            if (_awaitingTaskTitle)
            {
                _pendingTaskTitle = input;
                _awaitingTaskTitle = false;
                _awaitingReminderChoice = true;
                return $"Task added: '{_pendingTaskTitle}'. Would you like a reminder? (yes/no)";
            }

            // Step B — waiting for yes/no on reminder
            if (_awaitingReminderChoice)
            {
                _awaitingReminderChoice = false;

                if (input.ToLower().Contains("yes"))
                {
                    _db.AddTask(_pendingTaskTitle, _pendingTaskTitle, DateTime.Now.AddDays(7));
                    _activityLog.LogAction($"Task added: '{_pendingTaskTitle}' (Reminder set for 7 days from now)");
                    return "Got it! I'll remind you in 7 days.";
                }
                else
                {
                    _db.AddTask(_pendingTaskTitle, _pendingTaskTitle, null);
                    _activityLog.LogAction($"Task added: '{_pendingTaskTitle}' (no reminder set)");
                    return "Okay, no reminder set.";
                }
            }

            return "";
        }
    }
}
