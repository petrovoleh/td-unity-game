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

    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);

        if (sceneName == "Map1" || sceneName == "Map2")
        {
            PlayerPrefs.DeleteKey("ObjectCount");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
