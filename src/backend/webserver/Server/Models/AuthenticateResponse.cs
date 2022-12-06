namespace WebApi.Models;

using WebApi.Entities;

public class AuthenticateResponse
{
    public string Username { get; set; }
    public DateTime RegDate { get; set; }
    public float Level { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        Username = user.Username;
        RegDate = user.RegDate;
        Level = user.Level;
        Token = token;
    }
}