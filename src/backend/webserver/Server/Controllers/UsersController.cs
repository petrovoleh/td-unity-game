using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }
    [HttpPost("register")]
    public IActionResult Register(AuthenticateRequest model)
    {
        var users = _userService.Register(model);
        return Ok(users);
    }
    // [Authorize]
    // [HttpGet]
    // public IActionResult GetAll()
    // {
    //     var users = _userService.GetAll();
    //     return Ok(users);
    // }
    
    
}
