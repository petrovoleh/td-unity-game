using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTheTower : MonoBehaviour
{
    public GameObject Tower1;

    private void OnMouseDown()
    {      
        Debug.Log("OnMouseDown");
        GameObject TowerClone = Instantiate(Tower1);
            TowerClone.transform.position = new Vector3(GlobalVariables.selectedTowerPlace.transform.position.x, GlobalVariables.selectedTowerPlace.transform.position.y, 0);
        Destroy(GlobalVariables.selectedTowerPlace);
    } 
}
