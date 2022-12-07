namespace Server.DatabaseConnection;

using Npgsql;
using SharedLibrary;
public class DatabaseConnection
{
    private const string host = "Host=193.219.91.103;Port=7172;Username=webserver;Password=password123;Database=playersdata";
    private NpgsqlConnection connection;

    public DatabaseConnection()
    {
        //try to connect
        try
        {
            connection = new NpgsqlConnection(host);
            connection.Open();
            Console.WriteLine("connected");
        }
        catch (Exception e)
        {
            Console.WriteLine("server is not available");
            Environment.Exit(404);
        }
    }
    public async Task<Completed_maps> GetPlayerProgress(string username)
    {
        var maps = new Completed_maps();
        string command = $"SELECT * FROM completed_map WHERE username = '{username}'";
        await using var dataSource = NpgsqlDataSource.Create(host);
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            maps.map = new List<Map>();

            int count = 0;
            while (await reader.ReadAsync())
            {
                maps.map.Add(new Map(){Map_id = reader.GetInt32(1),Medals = reader.GetString(2),Difficulty = reader.GetInt32(3)});
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
                users.Add(new User(){Username = reader.GetString(0), Password=reader.GetString(1), Level = reader.GetFloat(2), RegDate = reader.GetDateTime(3)});
                count++;
            }
        }
        return users;
    }
}