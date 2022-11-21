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
            GameManager.Instance.SelectedTowerPlace = this.gameObject;
            Debug.Log(GameManager.Instance.SelectedTowerPlace);
            GameObject SelectedClone = Instantiate(Selected);
            SelectedClone.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            //Destroy(gameObject);
        }
       
        
    } 

    
}
