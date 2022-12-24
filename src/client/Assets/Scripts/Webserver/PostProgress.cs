using _Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SharedLibrary;
using System.Threading.Tasks;
public class PostProgress : MonoBehaviour
{
    public async void postProgress(){ //async
        PlayerProgress progress = await readFile<PlayerProgress>("progress.json");
        User user = await readFile<User>("logindata.json");
        progress.username = user.Username;
        progress = await HttpClient.Post<PlayerProgress>("http://193.219.91.103:5756/playermaps", progress, user.Token);

    }


    private async Task<T> readFile<T>(string file){
        var fileName = GetFilePath(file);

        using FileStream fs = File.OpenRead(fileName);
        using var sr = new StreamReader(fs);

        string json;
        json = sr.ReadLine();

        Debug.Log(json);
        return JsonUtility.FromJson<T>(json);
    }


    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
