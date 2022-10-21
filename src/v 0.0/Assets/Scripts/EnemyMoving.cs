using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public float movespeed = 0.2f;
    
    void Destroying(){
        if (GlobalVariables.playerHP > 0)
            GlobalVariables.playerHP -= 1;
        Destroy(gameObject);
    }
    void Update()
    {
        if (transform.position.x>-250 && transform.position.y>22)
            transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
        else if (transform.position.y>22)
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
        else if (transform.position.x < 220 && transform.position.y>-95)
            transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
        else if (transform.position.y>-95)
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
        else if (transform.position.x>-210)
            transform.position = new Vector3(transform.position.x - movespeed, transform.position.y); 
        else if (transform.position.y>-300)
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
        else Destroying();
    }
}
