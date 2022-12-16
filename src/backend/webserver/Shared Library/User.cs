using System.Text.Json.Serialization;

namespace SharedLibrary;
//using System.Text.Json.Serialization;

[Serializable]
public class User
{
    public string Username;

    //[JsonIgnore] 
    public string Password;

    public string Token;
}