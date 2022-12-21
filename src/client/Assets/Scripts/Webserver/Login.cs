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
        User user_tmp = await HttpClient.Get<User>("http://193.219.91.103:5756/users/authenticate", user);
        Debug.Log("user logged in");
        user.Token = user_tmp.Token;
        string loginLink = "http://193.219.91.103:5756/playermaps/"+user.Username;
        PlayerProgress maps = await HttpClient.GetAuth<PlayerProgress>(loginLink,user.Token);
        
        Save(maps,"progress.json");
        Save(user,"logindata.json");
    }

    public void Save(object obj, string fileName)
    {
        string json = JsonUtility.ToJson(obj);
        WriteToFile(fileName, json);
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
