using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerHP : MonoBehaviour
{
    
    public GameObject LoseTheGameScreen;
    public GameObject SpawnEnemies;
    public Text hp;
    void Defeat()
    {
        LoseTheGameScreen.SetActive(true);
        SpawnEnemies.SetActive(false);

    }
    // Update is called once per frame
    void Update()
<<<<<<< Updated upstream:src/v 0.0/Assets/Scripts/ChangePlayerHP.cs
    {
        if (GlobalVariables.playerHP == 0)
           Defeat();
        hp.text =  GlobalVariables.playerHP.ToString();
=======
    {/*
        if (GameManager.Instance.PlayerHP == 0)
           Defeat();
        hp.text = GameManager.Instance.PlayerHP.ToString();*/
>>>>>>> Stashed changes:src/v 0.0/Assets/Scripts/Game/ChangePlayerHP.cs
    }
    
}
