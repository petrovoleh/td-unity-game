using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlaceClick : MonoBehaviour
{
    public GameObject Tower1;

    private void OnMouseDown()
    {
        GameObject TowerClone = Instantiate(Tower1);
            TowerClone.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Destroy(gameObject);
        } 

    
}
