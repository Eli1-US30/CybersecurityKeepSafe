# CybersecurityChatbot — KeepSafe Bot

## Description
A WPF-based cybersecurity awareness chatbot that educates users on online safety topics 
through an interactive GUI interface.

## Author
- **Name: Eli Basson
- **Student Number: ST10494408
- **Course: PROG6221 - Programming 2A
- **Institution: Rosebank College

## Features
- Voice greeting on launch
- ASCII art logo in header
- Personalised responses using user's name
- Sentiment detection (worried, curious, frustrated)
- Keyword recognition with multiple random responses
- Follow-up phrases ("tell me more")
- Memory system that remembers your name
- Fallback responses for unrecognised input
## Part 3 Features (New)
- **Task Assistant** — Add cybersecurity-related tasks with optional reminders, stored in a MySQL database. View, complete, or delete tasks.
- **Cybersecurity Quiz** — 10 multiple-choice/true-false questions with immediate feedback and a final score.
- **NLP Simulation** — Recognizes varied phrasings for the same intent (e.g. "add a task", "remind me to...", "create a task").
- **Activity Log** — Tracks recent bot actions (tasks added, quiz attempts) and displays them on request.

  ## Database Setup
1. Install MySQL Server and Workbench
2. Create a schema named `cybersecurity_chatbot`
3. Run this SQL to create the tasks table:
```sql
CREATE TABLE tasks (
    TaskId INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description VARCHAR(500),
    ReminderDate DATE NULL,
    IsCompleted BOOLEAN DEFAULT FALSE
);
```
4. Update the connection string in `DatabaseHelper.cs` with your MySQL root password

## Topics The Bot Can Help With
- Passwords
- Phishing
- Viruses / Malware
- VPNs
- Firewalls

## Project Structure
CybersecurityChatbot/
├── MainWindow.xaml        - GUI layout
├── MainWindow.xaml.cs     - GUI event handlers
├── ChatBot.cs             - Core chatbot logic
├── KeywordResponder.cs    - Keyword responses
├── SentimentDetector.cs   - Sentiment detection
├── MemoryStore.cs         - User memory
└── greetings.wav          - Voice greeting

## Prerequisites
- Windows OS
- Visual Studio 2022
- .NET 8.0 or higher
- System.Windows.Extensions NuGet package

## How To Run
1. Clone the repository:https://github.com/Eli1-US30/CybersecurityKeepSafe.git
2. Open `CybersecurityChatbot.sln` in Visual Studio 2022
3. Make sure `greetings.wav` is in the project folder and set to Copy Always
4. Press `F5` to run

## How To Use
1. Launch the app — voice greeting plays automatically
2. Enter your name when prompted
3. Type any of the following to get information:
   - `password` / `phishing` / `virus` / `vpn` / `firewall`
   - `how are you` / `what can you do`
   - `tell me more` — get more info on the last topic
4. Try typing how you feel e.g. `I am worried about phishing`
## How To Use (Part 3 additions)
- `add task` / `remind me to...` — add a new task
- `show tasks` / `my tasks` — view all tasks
- `quiz` / `start quiz` — begin the cybersecurity quiz
- `activity log` / `show log` — view recent bot actions
