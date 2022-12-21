namespace Server.DatabaseConnection;
using Npgsql;
using SharedLibrary;
public class PostData
{
    //private const string host = "Host=193.219.91.103;Port=7172;Username=webserver;Password=password123;Database=playersdata";
    private const string host = "Host=10.0.0.186;Username=webserver;Password=password123;Database=playersdata";
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