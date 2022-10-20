using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerHP : MonoBehaviour
{
    public Text hp;
    // Update is called once per frame
    void Update()
    {
        
        hp.text =  GlobalVariables.playerHP.ToString();
    }
}
