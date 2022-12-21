using System.Collections;
using System.Collections.Generic;
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
        loginUI.SetActive(true);
    }

    public void LoginBack(){
        loginUI.SetActive(false);
    
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
