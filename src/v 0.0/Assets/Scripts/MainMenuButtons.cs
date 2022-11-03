using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public string sceneName;
     public GameObject optionsUI;
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
}
