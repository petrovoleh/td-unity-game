using _Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using SharedLibrary;
public class RegisterLogin : MonoBehaviour
{
    public GameObject inputLogin;
    public GameObject inputPassword;

    public GameObject LogInputLogin;
    public GameObject LogInputPassword;
    public GameObject loggedUser;
    public GameObject registerMenu;
    public GameObject loginMenu;
    public Text registerText;
    public Text loginText;
    public async void login(){ //async
        string login =LogInputLogin.GetComponent<Text>().text;
        string password =LogInputPassword.GetComponent<Text>().text;
        User user = new User();
        user.Password = password;
        user.Username = login;        

        int err = validateInput(login);
        if (err == 1 || err == 2){
            loginText.text ="login must be between 6 and 20 characters";
            return;
        }
        if (err == 3){
            loginText.text ="login contains unsupported characters";
            return;
        }
        err = validateInput(password);
        if (err == 1 || err == 2){
            loginText.text ="password must be between 6 and 20 characters";
            return;
        }
        if (err == 3){
            loginText.text ="password contains unsupported characters";
            return;
        }
        try{
        User user_tmp = await HttpClient.Post<User>("users/authenticate", user);

        Debug.Log("user logged in");
        user.Token = user_tmp.Token;
        string loginLink = "playermaps/"+user.Username;
        PlayerProgress maps = await HttpClient.Get<PlayerProgress>(loginLink,user.Token);
        
        save(maps,"progress.json");
        save(user,"logindata.json");

        }
        catch (Exception){
            Debug.Log("user has not been registred");
            loginText.text = "no internet connection or no user with the given username could be found";
            return;
        }

        UserData.user=user;
        loginMenu.SetActive(false);
        loggedUser.GetComponent<UpdateText>().updateText();
        loggedUser.SetActive(true);
    }

    public async void register(){ 
        string login = inputLogin.GetComponent<Text>().text;
        string password = inputPassword.GetComponent<Text>().text;
        User user = new User();
        user.Password = password;
        user.Username = login;


        int err = validateInput(login);
        if (err == 1 || err == 2){
            registerText.text = "login must be between 6 and 20 characters";
            return;
        }
        if (err == 3){
            registerText.text = "login contains unsupported characters";
            return;
        }
        err = validateInput(password);
        if (err == 1 || err == 2){
            registerText.text = "password must be between 6 and 20 characters";
            return;
        }
        if (err == 3){
            registerText.text = "password contains unsupported characters";
            return;
        }

        try{
            User user_tmp = await HttpClient.Post<User>("users/register", user);
            user.Token = user_tmp.Token;
            save(user,"logindata.json");
        }
        catch (Exception){
            Debug.Log("user has not been registred");
            registerText.text = "no internet connection";
            return;
        }
        
        Debug.Log("user registred");
        UserData.user=user;

        registerMenu.SetActive(false);
        loggedUser.GetComponent<UpdateText>().updateText();
        loggedUser.SetActive(true);
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

   public void save(object obj, string fileName)
    {
        string json = JsonUtility.ToJson(obj);
        writeToFile(fileName, json);
    }


    private void writeToFile(string fileName, string json)
    {
        string path = getFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }


    private string getFilePath(string fileName)
    {
        Debug.Log(Application.persistentDataPath + "/" + fileName);
        return Application.persistentDataPath + "/" + fileName;
    }
}
