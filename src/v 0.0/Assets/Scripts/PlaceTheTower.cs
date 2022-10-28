using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceTheTower : MonoBehaviour
{
    public GameObject Tower1;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceTxt;

    private void OnMouseDown()
    {      
        if(price <= GameManager.Instance.Currency && GlobalVariables.selectedTowerPlace != null && PauseMenu.GameIsPaused == false)
        {
            GameManager.Instance.Currency -= price;
            Debug.Log("OnMouseDown");
            GameObject TowerClone = Instantiate(Tower1);
            TowerClone.transform.position = new Vector3(GlobalVariables.selectedTowerPlace.transform.position.x, GlobalVariables.selectedTowerPlace.transform.position.y, 0);
            Destroy(GlobalVariables.selectedTowerPlace);
        }
        
    } 

    private void Start()
    {
        priceTxt.text = price + "";
    }
}
