using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerMapsController : ControllerBase
{
    private DatabaseConnection database;
    public PlayerMapsController()
    {
        database = new DatabaseConnection();
    }
    
    [HttpGet("{username}")]
    public async Task<Completed_maps> Get([FromRoute] string username)
    {
        Completed_maps maps = await database.GetPlayerProgress(username);
        Console.WriteLine("player maps data was downloaded");
      
        return maps;
    }

    [HttpPost]
    public Completed_maps Post(Completed_maps maps)
    {
        Console.WriteLine("Player maps was posted");
        return maps;
    }
}