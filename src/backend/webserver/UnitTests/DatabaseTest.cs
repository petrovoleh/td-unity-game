using Server.DatabaseConnection;
using SharedLibrary;

namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SelectTest()
    {
        List<string> names = new List<string>(){
            "player1",
            "player2",
            "player3",
            "player4"
        };
        
        foreach (var username in names)
        {
            PlayerProgress progress = DownloadData.GetPlayerProgress(username).Result;
        }

        Assert.Pass();
    }

    [Test]
    public void UserRegestrationTest()
    {
        User user = new User();
        user.Username = "test";
        user.Password = "test";
        
        Console.WriteLine(user.Username);
        PostData.PostUserData(user);

        var users = DownloadData.GetUserData().Result;
        User testuser = users.FirstOrDefault(x => x.Username == "test");
        if (testuser != null)
            Assert.Pass();
        else
            Assert.Fail();
        
    }
    [Test]
    public void InsertTest()
    {
        PlayerProgress progress = new PlayerProgress();

        Map map = new Map();
        map.Map_id = 1;
        map.Difficulty = 1;
        progress.maps = new List<Map>(){map};
        progress.username = "test";
        
        PostData.PostProgress(progress);
        
        PlayerProgress testprogress = DownloadData.GetPlayerProgress("test").Result;
        if(testprogress.maps!=null)
            Assert.Pass();
        else
            Assert.Fail();
    }
}