using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public Text gameObject;
    void Start()
    {
        gameObject.text = "Welcome, "+ MenuManager.user.Username+ "!";
    }

}