using System;
using System.Collections.Generic;

public class ActivityLogger
{
    private List<string> _log = new List<string>();

    public void LogAction(string description)
    {
        string entry = $"{DateTime.Now:HH:mm} - {description}";
        _log.Add(entry);
    }

    public string GetRecentLog(int count = 10)
    {
        if (_log.Count == 0)
            return "No actions logged yet.";

        int start = Math.Max(0, _log.Count - count);
        string result = "Here's a summary of recent actions:\n";

        for (int i = start; i < _log.Count; i++)
        {
            result += $"{i - start + 1}. {_log[i]}\n";
        }
        return result;
    }
}