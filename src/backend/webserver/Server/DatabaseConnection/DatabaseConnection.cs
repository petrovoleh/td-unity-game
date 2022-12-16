namespace Server.DatabaseConnection;

using Npgsql;
using SharedLibrary;
public class DatabaseConnection
{
    private const string host = "Host=193.219.91.103;Port=7172;Username=webserver;Password=password123;Database=playersdata";
    private NpgsqlConnection connection;

    // public DatabaseConnection()
    // {
    //     //try to connect
    //     try
    //     {
    //         connection = new NpgsqlConnection(host);
    //         connection.Open();
    //         Console.WriteLine("connected");
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine("server is not available");
    //         Environment.Exit(404);
    //     }
    // }
    public static async Task<BeatenMaps> GetPlayerProgress(string username)
    {
        var maps = new BeatenMaps();
        string command = $"SELECT * FROM beaten_map WHERE username = '{username}'";
        await using var dataSource = NpgsqlDataSource.Create(host);
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            maps.maps = new List<Map>();

            int count = 0;
            while (await reader.ReadAsync())
            {
                maps.maps.Add(new Map(){Map_id = reader.GetInt32(1),Difficulty = reader.GetInt32(2)});
                count++;
            }
        }
        
        command = $"SELECT * FROM completed_challenge WHERE username = '{username}'";
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            maps.challenges = new List<Challenge>();

            int count = 0;
            while (await reader.ReadAsync())
            {
                maps.challenges.Add(new Challenge(){Challenge_id = reader.GetInt32(1),Difficulty = reader.GetInt32(2)});
                count++;
            }
        }
        return maps;
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