using System;
using System.Collections.Generic;
using System.Text;

namespace CybersecurityChatbot
{
        public class KeywordResponder
        {
            private Dictionary<string, List<string>> _responses = new Dictionary<string, List<string>>
    {
        { "password", new List<string> {
            "Use strong passwords with 12+ characters, mixing letters, numbers and symbols.",
            "Never reuse passwords across different sites!",
            "Consider using a password manager to store your passwords safely.",
            "Avoid using personal info like birthdays in your passwords.",
            "Enable two-factor authentication alongside strong passwords."
        }},
        { "phishing", new List<string> {
            "Phishing is when attackers pretend to be trusted sources to steal your info.",
            "Never click suspicious links in emails or messages!",
            "Always verify the sender's email address before clicking anything.",
            "Legitimate companies will never ask for your password via email.",
            "When in doubt, go directly to the website instead of clicking links."
        }},
        { "virus", new List<string> {
            "Always keep your antivirus software updated.",
            "Never download files from unknown or untrusted sources.",
            "Malware can steal your data or damage your system.",
            "Run regular scans on your computer to detect threats early.",
            "Avoid clicking pop-up ads as they can contain malware."
        }},
        { "vpn", new List<string> {
            "A VPN encrypts your internet connection keeping your data private.",
            "Always use a VPN on public Wi-Fi networks.",
            "A VPN hides your IP address from websites and trackers.",
            "Not all VPNs are equal - choose a reputable paid service.",
            "A VPN protects your data from hackers on unsecured networks."
        }},
        { "firewall", new List<string> {
            "A firewall monitors network traffic and blocks unauthorized access.",
            "Always keep your firewall enabled on your device.",
            "Firewalls act as a barrier between your device and the internet.",
            "Both hardware and software firewalls are important for security.",
            "A firewall can block malicious traffic before it reaches your system."
        }},
        { "cloud", new List<string> {
            "Cloud storage stores your data on remote servers. Always use strong passwords to protect your accounts.",
            "Be careful what you store in the cloud. Sensitive data should always be encrypted before uploading.",
            "Use two-factor authentication on all your cloud accounts like Google Drive and OneDrive.",
            "Check the privacy settings on your cloud storage regularly to control who can access your files.",
            "Never share cloud storage links publicly as anyone with the link can access your files."
        }},
        { "privacy", new List<string> {
            "Regularly review the privacy settings on all your social media accounts.",
            "Never share personal information like your ID number or address online.",
            "Use a private browser or incognito mode when using public computers.",
            "Be careful what you post online — once it's there it can be very hard to remove.",
            "Read privacy policies before signing up to new apps or websites."
        }},
    };

            private Random _random = new Random();
            private string _lastKeyword = "";

            public string GetResponse(string input)
            {
                foreach (var keyword in _responses.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        _lastKeyword = keyword;
                        var list = _responses[keyword];
                        return list[_random.Next(list.Count)];
                    }
                }
                return "";
            }

            public string GetFollowUp()
            {
                if (_lastKeyword == "") return "";
                var list = _responses[_lastKeyword];
                return list[_random.Next(list.Count)];
            }
        }
    }
