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

    public GameObject LogInputLogin;
    public GameObject LogInputPassword;
    public async void login(){ //async
        string login =LogInputLogin.GetComponent<Text>().text;
        string password =LogInputPassword.GetComponent<Text>().text;
        User user = new User();
        user.Password = password;
        user.Username = login;        

        int err = validateInput(login);
        if (err == 1 || err == 2){
            Debug.Log("login must be between 6 and 20 characters");
            return;
        }
        if (err == 3){
            Debug.Log("login contains unsupported characters");
            return;
        }
        err = validateInput(password);
        if (err == 1 || err == 2){
            Debug.Log("password must be between 6 and 20 characters");
            return;
        }
        if (err == 3){
            Debug.Log("password contains unsupported characters");
            return;
        }
        User user_tmp;

        user_tmp = await HttpClient.Post<User>("http://193.219.91.103:5756/users/authenticate", user);

        Debug.Log("user logged in");
        user.Token = user_tmp.Token;
        string loginLink = "http://193.219.91.103:5756/playermaps/"+user.Username;
        PlayerProgress maps = await HttpClient.Get<PlayerProgress>(loginLink,user.Token);
        
        Save(maps,"progress.json");
        Save(user,"logindata.json");
    }

    public async void register(){ 
        string login =inputLogin.GetComponent<Text>().text;
        string password =inputPassword.GetComponent<Text>().text;
        User user = new User();
        user.Password = password;
        user.Username = login;


        int err = validateInput(login);
        if (err == 1 || err == 2){
            Debug.Log("login must be between 6 and 20 characters");
            return;
        }
        if (err == 3){
            Debug.Log("login contains unsupported characters");
            return;
        }
        err = validateInput(password);
        if (err == 1 || err == 2){
            Debug.Log("password must be between 6 and 20 characters");
            return;
        }
        if (err == 3){
            Debug.Log("password contains unsupported characters");
            return;
        }


        User user_tmp = await HttpClient.Post<User>("http://193.219.91.103:5756/users/register", user);
        Debug.Log("user registred");
        user.Token = user_tmp.Token;
        Save(user,"logindata.json");
    }

    private int validateInput(string input){
        if (string.IsNullOrEmpty(input))
            return 1;
        if (input.Length < 6 || input.Length > 20)
            return 2;
        string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_, ";
        foreach (var item in specialChar)
        {
            if (input.Contains(item)) return 3;
        }
        return 0;
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
