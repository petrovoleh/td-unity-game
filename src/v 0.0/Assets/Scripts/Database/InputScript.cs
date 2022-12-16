using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    private string input;
    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input.ToString());
    }

}
