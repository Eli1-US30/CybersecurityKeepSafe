using System.Media;
using System.Windows;
using System.Windows.Input;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private ChatBot _chatBot;

        public MainWindow()
        {
            InitializeComponent();
            _chatBot = new ChatBot();

            // Play greeting
            PlayGreeting();

            // Show opening message
            AppendBotMessage(_chatBot.GetGreeting());
        }

        private void PlayGreeting()
        {
            try
            {
#pragma warning disable CA1416
                SoundPlayer player = new SoundPlayer("greetings.wav");
                player.Play();
#pragma warning restore CA1416
            }
            catch { }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendMessage()
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AppendUserMessage(input);
            UserInput.Text = "";

            string response = _chatBot.ProcessInput(input);
            AppendBotMessage(response);
        }

        private void AppendUserMessage(string message)
        {
            ChatDisplay.Text += $"\nYou: {message}\n";
            ChatScroller.ScrollToBottom();
        }

        private void AppendBotMessage(string message)
        {
            ChatDisplay.Text += $"\nBot: {message}\n";
            ChatScroller.ScrollToBottom();
        }
    }
}