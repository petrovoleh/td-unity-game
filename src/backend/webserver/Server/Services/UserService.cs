using Server.DatabaseConnection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SharedLibrary;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    AuthenticateResponse Register(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(string username);
}

public class UserService : IUserService
{
    private List<User> _users = DatabaseConnection.GetUserData().Result;


    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }
    public AuthenticateResponse Register(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username);

        // return null if user not found
        if (user != null) return null;
        user = new User();
        user.Username = model.Username;
        user.Password = model.Password;
        
        Console.WriteLine(user.Username);
        PostData.PostUserData(user);
        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }
    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User GetById(string username)
    {
        return _users.FirstOrDefault(x => x.Username == username);
    }

    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("username", user.Username) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}