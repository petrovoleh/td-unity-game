using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    /*[SerializeField] 
    private bool unlocked;

    if(unlocked == true)
        {
        LockedButton.gameObject.SetActive(false);
        }*/
   
    public string sceneName;
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
