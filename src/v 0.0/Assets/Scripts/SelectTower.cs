using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    public GameObject Selected;

    private void OnMouseDown()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            GlobalVariables.selectedTowerPlace = this.gameObject;
            Debug.Log(GlobalVariables.selectedTowerPlace);
            GameObject SelectedClone = Instantiate(Selected);
            SelectedClone.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            //Destroy(gameObject);
        }
       
        
    } 

    
}
