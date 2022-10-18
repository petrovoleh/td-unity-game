using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerHP : MonoBehaviour
{
    //Lost the game screen
    public GameObject lossScreen;
    public Text hp;
    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.playerHP>0)
        hp.text =  GlobalVariables.playerHP.ToString();
        else
        lossScreen.SetActive(true);
    }
}
