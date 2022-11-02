using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelection : MonoBehaviour
{
    bool not_in_object = false;
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && (gameObject.name != "Selected") && not_in_object){
            Destroy(gameObject);
        }
    }
    void OnMouseExit()
    {
        
        not_in_object=true;
        
    }
}
