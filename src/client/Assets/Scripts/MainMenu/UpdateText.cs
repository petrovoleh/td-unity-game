using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    public Text text;
    void Start()
    {
        updateText();
    }
    public void updateText(){
        text.text = "Welcome, "+ MenuManager.user.Username+ "!";
    }

}