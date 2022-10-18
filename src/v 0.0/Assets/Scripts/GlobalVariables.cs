using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    //this variable writes selected tower when you clicking on it(SelectTower.cs)and when you choose new tower (PlaceTheTower.cs) - delete place
    public static GameObject selectedTowerPlace;
    //player Hit points
    public static int playerHP = 20;
}
