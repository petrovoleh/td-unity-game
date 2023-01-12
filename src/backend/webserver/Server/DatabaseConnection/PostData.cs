using System.Text;
using Microsoft.Extensions.Options;

namespace Server.DatabaseConnection;
using Npgsql;
using SharedLibrary;
public class PostData
{
    private static AppSettings _appSettings;
    private static string ip;
    private static string port;
    private static string password;
    private static string db;
    private static readonly string host = $"Host={ip};Username=webserver;Password={password};Database={db}";

    public PostData(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        ip = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
        port = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
        password = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
        db = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
    }
    public static async Task PostUserData(User user)
    {
        await using var conn = new NpgsqlConnection(host);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand("INSERT INTO Player(username,password) VALUES (($1), ($2))", conn)
        {
            Parameters =
            {
                new() { Value = user.Username },
                new() { Value = user.Password }
            }
        };
        await cmd.ExecuteNonQueryAsync();
    }
    public static async Task PostProgress(PlayerProgress progress)
    {
        await using var conn = new NpgsqlConnection(host);
        await conn.OpenAsync();
        for (int i = 0; i < progress.maps.Count; i++)
        {
            await using var cmd =
                new NpgsqlCommand("INSERT INTO Beaten_map(username, map_id, difficulty, beat_id) VALUES (($1), ($2), ($3), ($4)) ON CONFLICT (beat_id) DO UPDATE SET difficulty = ($3)", conn)
                {
                    Parameters =
                    {
                        new() { Value = progress.username },
                        new() { Value = progress.maps[i].Map_id },
                        new() { Value = progress.maps[i].Difficulty },
                        new() { Value = progress.username+progress.maps[i].Map_id },
                    }
                };
            await cmd.ExecuteNonQueryAsync();
        }
        
        for (int i = 0; i < progress.challenges.Count; i++)
        {
            await using var cmd =
                new NpgsqlCommand("INSERT INTO completed_challenge(username, challenge_id, difficulty, beat_id) VALUES (($1), ($2), ($3), ($4))  ON CONFLICT (beat_id) DO UPDATE SET difficulty = ($3)", conn)
                {
                    Parameters =
                    {
                        new() { Value = progress.username },
                        new() { Value = progress.challenges[i].Challenge_id },
                        new() { Value = progress.challenges[i].Difficulty },
                        new() { Value = progress.username+progress.challenges[i].Challenge_id },
                    }
                };
            await cmd.ExecuteNonQueryAsync();
        }
    }
    
}