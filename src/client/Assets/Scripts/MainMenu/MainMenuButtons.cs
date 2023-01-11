using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    /*[SerializeField] 
    private bool unlocked;

    if(unlocked == true)
        {
        LockedButton.gameObject.SetActive(false);
        }*/
   
    public string sceneName;
    public GameObject optionsUI;
    public GameObject loginUI;
    public GameObject registerMenu;
    public GameObject loginMenu;
    public GameObject loggedUser;

    [SerializeField]
    private GameObject LoadGameButton;
    public void Start()
    {
        if (PlayerPrefs.HasKey("ObjectCount"))
        {
            LoadGameButton.SetActive(true);
        }
        else
        {
            LoadGameButton.SetActive(false);
        }
    }
    public void LoadGame()
    {
        PlayerPrefs.SetString("Loaded", "Yes");
        SceneManager.LoadScene(PlayerPrefs.GetString("MapName"));
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Options()
    {
        optionsUI.SetActive(true);
    }

    public void OptionsBack(){
        optionsUI.SetActive(false);
    
    }
    public void Login()
    {
        if(UserData.user == null)
            loginUI.SetActive(true);
        else
            loggedUser.SetActive(true);
    }

    public void LoginBack(){
        loginUI.SetActive(false);
    }
    public void LoggedUserBack(){
        loggedUser.SetActive(false);
    }
    public void SignOut(){
        this.gameObject.GetComponent<PostProgress>().postProgress();
        UserData.user = null;
        loggedUser.SetActive(false);
        File.Delete(Application.persistentDataPath + "/logindata.json");
        loginUI.SetActive(true);
        
    }

    public void LoginMenu()
    {
        loginMenu.SetActive(true);
    }

    public void LoginMenuBack(){
        loginMenu.SetActive(false);
    }

    public void RegisterMenu()
    {
        registerMenu.SetActive(true);
    }

    public void RegisterMenuBack(){
        registerMenu.SetActive(false);
    
    }
}
