using Microsoft.AspNetCore.Mvc;
using SharedLibrary;
using Server.DatabaseConnection;

[ApiController]
[Route("[controller]")]
public class PlayerMapsController : ControllerBase
{
    [Authorize]
    [HttpGet("{username}")]
    public async Task<BeatenMaps> Get([FromRoute] string username)
    {
        BeatenMaps maps = await DatabaseConnection.GetPlayerProgress(username);
        Console.WriteLine(username +" maps data was downloaded");
      
        return maps;
    }

    [HttpPost]
    public BeatenMaps Post(BeatenMaps maps)
    {
        Console.WriteLine("Player maps was posted");
        return maps;
    }
}