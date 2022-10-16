using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    float movespeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x>-250 && transform.position.y>22)
            transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
        else if (transform.position.y>22)
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
        else if (transform.position.x < 220 && transform.position.y>-95)
            transform.position = new Vector3(transform.position.x+ movespeed, transform.position.y);
        else if (transform.position.y>-95)
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
        else if (transform.position.x>-210)
            transform.position = new Vector3(transform.position.x - movespeed, transform.position.y); 
        else if (transform.position.y>-300)
            transform.position = new Vector3(transform.position.x, transform.position.y - movespeed);
    }
}
