using System.Text;
using Microsoft.Extensions.Options;

namespace Server.DatabaseConnection;

using Npgsql;
using SharedLibrary;
public class DownloadData
{
    private static AppSettings _appSettings;
    private static string ip;
    private static string port;
    private static string password;
    private static string db;
    private static readonly string host = $"Host={ip};Username=webserver;Password={password};Database={db}";

    public DownloadData(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        ip = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
        port = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
        password = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
        db = Encoding.ASCII.GetBytes(_appSettings.Secret).ToString();
    }
    public static async Task<PlayerProgress> GetPlayerProgress(string username)
    {
        var progress = new PlayerProgress();
        string command = $"SELECT * FROM beaten_map WHERE username = '{username}'";
        await using var dataSource = NpgsqlDataSource.Create(host);
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            progress.maps = new List<Map>();

            int count = 0;
            while (await reader.ReadAsync())
            {
                progress.maps.Add(new Map(){Map_id = reader.GetInt32(1),Difficulty = reader.GetInt32(2)});
                count++;
            }
        }
        
        command = $"SELECT * FROM completed_challenge WHERE username = '{username}'";
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            progress.challenges = new List<Challenge>();

            int count = 0;
            while (await reader.ReadAsync())
            {
                progress.challenges.Add(new Challenge(){Challenge_id = reader.GetInt32(1),Difficulty = reader.GetInt32(2)});
                count++;
            }
        }
        return progress;
    }
    public static async Task<List<User>> GetUserData()
    {
        List<User> users = new List<User>();
        string command = $"SELECT * FROM player";
        await using var dataSource = NpgsqlDataSource.Create(host);
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {

            int count = 0;
            while (await reader.ReadAsync())
            {
                users.Add(new User(){Username = reader.GetString(0), Password=reader.GetString(1)});
                count++;
            }
        }
        return users;
    }
}