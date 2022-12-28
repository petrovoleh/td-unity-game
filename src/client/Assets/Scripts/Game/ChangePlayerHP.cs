using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerHP : MonoBehaviour
{
    public GameObject LoseTheGameScreen;
    public Text hp;
    void Defeat()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        LoseTheGameScreen.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerHP <= 0)
           Defeat();
        hp.text = GameManager.Instance.PlayerHP.ToString();
    }
    
}
