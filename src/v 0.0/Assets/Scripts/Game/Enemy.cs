using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float movespeed;

    public float MaxSpeed { get; set; }
    public float Movespeed
    {
        get
        {
            return movespeed;
        }
        set
        {
            this.movespeed = value;
        }
    }

    private Waypoints Wpoints;

    [SerializeField]
    private int health;

    public bool Alive
    {
        get { if(health > 0){ return true; } return false; }
    }
    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> debuffsToAdd = new List<Debuff>();

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
        HandleDebuffs();

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
    }
    
    public void Spawn()
    {
        transform.position = new Vector3(450, 93);
    }
    private void Awake()
    {
        MaxSpeed = movespeed;
    }
    public void TakeDamage(float damage)
    {
        for(; damage > 0; damage--)
        {
            GameManager.Instance.Currency += 1;
            health--;
            if (health <= 0) { break; }
                
        }
        
        if(health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.enemyCount--;
            Debug.Log(GameManager.Instance.enemyCount);
            GameManager.Instance.RemoveMonster(this);
        }
    }
    
    public void AddDebuff(Debuff debuff)
    {
        if(!debuffs.Exists(x => x.GetType() == debuff.GetType()))
        {
            debuffsToAdd.Add(debuff);
        }
    }
    public void RemoveDebuff(Debuff debuff)
    {
        debuffsToRemove.Add(debuff);
    }
    private void HandleDebuffs()
    {
        if(debuffsToAdd.Count > 0)
        {
            debuffs.AddRange(debuffsToAdd);

            debuffsToAdd.Clear();
        }
        foreach(Debuff debuff in debuffsToRemove)
        {
            debuffs.Remove(debuff);
        }
        debuffsToRemove.Clear();

        foreach(Debuff debuff in debuffs)
        {
            debuff.Update();
        }
    }
}
