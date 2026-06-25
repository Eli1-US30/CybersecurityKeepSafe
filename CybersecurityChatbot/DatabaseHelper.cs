using MySql.Data.MySqlClient;
using System.Windows.Controls;

public class DatabaseHelper
{
    private string connectionString = "server=localhost;user=root;password=ELI-HUNDERD-93;database=cybersecurity_chatbot;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
    // ── ADD A TASK ──────────────────────────────
    public void AddTask(string title, string description, DateTime? reminderDate)
    {
        using (var conn = GetConnection())
        {
            conn.Open();
            string query = "INSERT INTO tasks (Title, Description, ReminderDate, IsCompleted) VALUES (@title, @desc, @reminder, false)";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@reminder", reminderDate ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // ── GET ALL TASKS ───────────────────────────
    public List<TaskItem> GetAllTasks()
    {
        List<TaskItem> tasks = new List<TaskItem>();

        using (var conn = GetConnection())
        {
            conn.Open();
            string query = "SELECT * FROM tasks";
            using (var cmd = new MySqlCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tasks.Add(new TaskItem
                    {
                        TaskId = reader.GetInt32("TaskId"),
                        Title = reader.GetString("Title"),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString("Description"),
                        ReminderDate = reader.IsDBNull(reader.GetOrdinal("ReminderDate")) ? (DateTime?)null : reader.GetDateTime("ReminderDate"),
                        IsCompleted = reader.GetBoolean("IsCompleted")
                    });
                }
            }
        }
        return tasks;
    }

    // ── DELETE A TASK ───────────────────────────
    public void DeleteTask(int taskId)
    {
        using (var conn = GetConnection())
        {
            conn.Open();
            string query = "DELETE FROM tasks WHERE TaskId = @id";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // ── MARK TASK COMPLETE ──────────────────────
    public void CompleteTask(int taskId)
    {
        using (var conn = GetConnection())
        {
            conn.Open();
            string query = "UPDATE tasks SET IsCompleted = true WHERE TaskId = @id";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id", taskId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}