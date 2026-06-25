using System;
using System.Collections.Generic;

public class QuizGame
{
    private List<QuizQuestion> _questions;
    private int _currentIndex = 0;
    private int _score = 0;
    public int Score => _score;
    public int TotalQuestions => _questions.Count;
    public bool IsActive { get; private set; } = false;

    public QuizGame()
    {
        _questions = new List<QuizQuestion>
        {
            new QuizQuestion("What should you do if you receive an email asking for your password?",
                new List<string> { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" },
                "C", "Reporting phishing emails helps prevent scams."),

            new QuizQuestion("True or False: It's safe to use the same password for multiple accounts.",
                new List<string> { "A) True", "B) False" },
                "B", "Using unique passwords prevents one breach from affecting all your accounts."),

            new QuizQuestion("What does VPN stand for?",
                new List<string> { "A) Virtual Private Network", "B) Very Personal Network", "C) Virtual Public Network", "D) Verified Private Node" },
                "A", "A VPN encrypts your connection and hides your IP address."),

            new QuizQuestion("True or False: Public Wi-Fi is always safe to use for banking.",
                new List<string> { "A) True", "B) False" },
                "B", "Public Wi-Fi can be intercepted by attackers - avoid sensitive activity on it."),

            new QuizQuestion("What is two-factor authentication?",
                new List<string> { "A) Using two passwords", "B) An extra verification step beyond your password", "C) Logging in twice", "D) A type of firewall" },
                "B", "2FA adds an extra layer of security beyond just your password."),

            new QuizQuestion("What is phishing?",
                new List<string> { "A) A type of antivirus", "B) A fishing hobby", "C) Tricking users into giving up personal info", "D) A firewall setting" },
                "C", "Phishing attacks trick users into revealing sensitive information."),

            new QuizQuestion("True or False: You should click links in unexpected emails to verify they are safe.",
                new List<string> { "A) True", "B) False" },
                "B", "Never click suspicious links - verify through official channels instead."),

            new QuizQuestion("What is malware?",
                new List<string> { "A) Malicious software", "B) A hardware part", "C) A type of password", "D) A network cable" },
                "A", "Malware is software designed to harm or exploit systems."),

            new QuizQuestion("What's a strong password practice?",
                new List<string> { "A) Using your birthday", "B) Using 'password123'", "C) Using 12+ characters with symbols and numbers", "D) Using your name" },
                "C", "Long, complex passwords are much harder to crack."),

            new QuizQuestion("True or False: A firewall can help block unauthorized access to your network.",
                new List<string> { "A) True", "B) False" },
                "A", "Firewalls monitor and control incoming/outgoing network traffic.")
        };
    }

    public string Start()
    {
        IsActive = true;
        _currentIndex = 0;
        _score = 0;
        return GetCurrentQuestionText();
    }

    private string GetCurrentQuestionText()
    {
        var q = _questions[_currentIndex];
        string text = $"Question {_currentIndex + 1}/{_questions.Count}:\n{q.QuestionText}\n";
        foreach (var option in q.Options)
            text += option + "\n";
        return text;
    }

    public string SubmitAnswer(string answer)
    {
        var q = _questions[_currentIndex];
        string cleanedAnswer = answer.Trim().ToUpper().Replace(")", "");

        string feedback;
        if (cleanedAnswer.StartsWith(q.CorrectAnswer))
        {
            _score++;
            feedback = $"Correct! {q.Explanation}";
        }
        else
        {
            feedback = $"Incorrect. The correct answer was {q.CorrectAnswer}. {q.Explanation}";
        }

        _currentIndex++;

        if (_currentIndex >= _questions.Count)
        {
            IsActive = false;
            string finalMessage = GetFinalScoreMessage();
            return $"{feedback}\n\nQuiz complete! Your score: {_score}/{_questions.Count}\n{finalMessage}";
        }

        return $"{feedback}\n\n{GetCurrentQuestionText()}";
    }

    private string GetFinalScoreMessage()
    {
        double percentage = (double)_score / _questions.Count * 100;
        if (percentage >= 80) return "Great job! You're a cybersecurity pro!";
        if (percentage >= 50) return "Good effort! Keep learning to stay safe online.";
        return "Keep learning to stay safe online - review the topics and try again!";
    }
}

public class QuizQuestion
{
    public string QuestionText { get; set; }
    public List<string> Options { get; set; }
    public string CorrectAnswer { get; set; }
    public string Explanation { get; set; }

    public QuizQuestion(string questionText, List<string> options, string correctAnswer, string explanation)
    {
        QuestionText = questionText;
        Options = options;
        CorrectAnswer = correctAnswer;
        Explanation = explanation;
    }
}