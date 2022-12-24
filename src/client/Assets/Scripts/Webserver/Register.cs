using _Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SharedLibrary;
public class Register : MonoBehaviour
{
    public GameObject inputLogin;
    public GameObject inputPassword;
    public async void register(){ 
        string login =inputLogin.GetComponent<Text>().text;
        string password =inputPassword.GetComponent<Text>().text;
        User user = new User();
        user.Password = password;
        user.Username = login;
        User user_tmp = await HttpClient.Post<User>("https://localhost:4000/users/register", user);
        Debug.Log("user registred");
        user.Token = user_tmp.Token;
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
