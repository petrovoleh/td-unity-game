namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public string Username;
    public float Level;
    public DateTime RegDate;
    

    [JsonIgnore] public string Password;
}