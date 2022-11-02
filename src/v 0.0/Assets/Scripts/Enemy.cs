using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movespeed = 0.2f;

    private Waypoints Wpoints;

    [SerializeField]
    private int health;

    private int waypointIndex = 0;
    void Start()
    {
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }
    
    void Destroying()
    {
        GameManager.Instance.enemyCount--;
        GameManager.Instance.RemoveMonster(this);
        if (GameManager.Instance.PlayerHP > 0)
            GameManager.Instance.PlayerHP -= 1;
        Destroy(gameObject);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointIndex].position, movespeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, Wpoints.waypoints[waypointIndex].position) < 0.1f)
        {
             if(waypointIndex < Wpoints.waypoints.Length - 1)
            {
                waypointIndex++;
            }
            else
            {
                Destroying();
                GameManager.Instance.activateWaveBtn();
            }
        }
        /*if (transform.position.x>-250 && transform.position.y>22)
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
        else Destroying();*/
    }
    
    public void Spawn()
    {
        transform.position = new Vector3(350, 145);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.enemyCount--;
            Debug.Log(GameManager.Instance.enemyCount);
            GameManager.Instance.RemoveMonster(this);
            
            
        }
    }
    
}
