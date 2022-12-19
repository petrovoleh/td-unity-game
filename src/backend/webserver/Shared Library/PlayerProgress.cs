namespace SharedLibrary;
[Serializable]
public class Map
{
    public int Map_id;
    public int Difficulty;

}
[Serializable]
public class Challenge
{
    public int Challenge_id;
    public int Difficulty;

}
[Serializable]
public class PlayerProgress
{
    public string username;
    public List<Map> maps;
    public List<Challenge> challenges;
}