using _Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SharedLibrary;
public class Login : MonoBehaviour
{
    public GameObject inputLogin;
    public GameObject inputPassword;
    public async void login(){ //async
        string login =inputLogin.GetComponent<Text>().text;
        string password =inputPassword.GetComponent<Text>().text;
        User user = new User();
        user.Password = password;
        user.Username = login;
        User user_tmp = await HttpClient.Get<User>("https://localhost:4000/users/authenticate", user);
        Debug.Log("user logged in");
        user.Token = user_tmp.Token;
        BeatenMaps maps = await HttpClient.GetAuth<BeatenMaps>("https://localhost:4000/playermaps/player2",user.Token);
        
        Save(maps);
        Save(user);
    }

    public void Save(object maps)
    {
        string json = JsonUtility.ToJson(maps);
        WriteToFile("progress.json", json);
    }


    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }


    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
