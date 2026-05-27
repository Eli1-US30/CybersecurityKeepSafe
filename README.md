# CybersecurityChatbot — KeepSafe Bot

## Description
A WPF-based cybersecurity awareness chatbot that educates users on online safety topics 
through an interactive GUI interface.

## Author
- **Name:** Your Name Here
- **Student Number:** Your Student Number Here
- **Course:** PROG6221 - Programming 2A
- **Institution:** Your University Here

## Features
- Voice greeting on launch
- ASCII art logo in header
- Personalised responses using user's name
- Sentiment detection (worried, curious, frustrated)
- Keyword recognition with multiple random responses
- Follow-up phrases ("tell me more")
- Memory system that remembers your name
- Fallback responses for unrecognised input

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
