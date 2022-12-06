using Npgsql;

public class DbServer
{
    private const string host = "Host=193.219.91.103;Port=7172;Username=webserver;Password=password123;Database=playersdata";
    private NpgsqlConnection connection;

    public DbServer()
    {
        //try to connect
        try
        {
            connection = new NpgsqlConnection(host);
            connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine("server is not available");
            Environment.Exit(404);
        }
    }
    public async Task getPlayerProgress()
    {
        
        string username = Console.ReadLine();
        string command = $"SELECT * FROM completed_map WHERE username = '{username}'";
        await using var dataSource = NpgsqlDataSource.Create(host);
        await using (var cmd = dataSource.CreateCommand(command))
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                Console.WriteLine("{0} {1} {2}", reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3));
            }
        }
    }
}