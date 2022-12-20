using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Basic, Slime, Hardened, Ghost, SlimeKing, TrollKing, Dracula, Golem, Dragon };
    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private float movespeed;

    [SerializeField]
    private int worth;

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

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            this.health = value;
        }
    }

    public bool Alive
    {
        get { if(health > 0){ return true; } return false; }
    }
    private List<Debuff> debuffs = new List<Debuff>();
    private List<Debuff> debuffsToRemove = new List<Debuff>();
    private List<Debuff> debuffsToAdd = new List<Debuff>();

    private int waypointIndex = 0;
    public int WaypointIndex
    {
        get
        {
            return waypointIndex;
        }
        set
        {
            this.waypointIndex = value;
        }
    }
    private float distanceToWaypoint;


    void Start()
    {
        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();
    }
    
    void Destroying()
    {
        GameManager.Instance.enemyCount--;
        GameManager.Instance.RemoveMonster(this);
        if (GameManager.Instance.PlayerHP > 0)
            GameManager.Instance.PlayerHP -= health;
        Destroy(gameObject);
    }
    void Update()
    {
        HandleDebuffs();
        distanceToWaypoint = Vector3.Distance(transform.position, Wpoints.waypoints[waypointIndex].position);
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
        //transform.position = new Vector3(450, 93);
        transform.position = new Vector3(GameManager.Instance.SpawnpointX, GameManager.Instance.SpawnpointY);
    }
    private void Awake()
    {
        MaxSpeed = movespeed;
    }
    public void TakeDamage(float damage, string AttackType)
    {

        if (enemyType == EnemyType.Basic)
        {
            for (; damage > 0; damage--)
            {
                health--;
                if (health <= 0) { break; }
            }
        }
        if (enemyType == EnemyType.SlimeKing)
        {
            for (; damage > 0; damage--)
            {
                StartCoroutine(GameManager.Instance.SlimeKingDamagedSpawn());
                health--;  
                if (health <= 0) { break; }
            }
        }
        if (enemyType == EnemyType.TrollKing)
        {
            for (; damage > 0; damage--)
            {
                health--;
                if (health <= 0) { break; }
            }
        }
        if (enemyType == EnemyType.Hardened)
        {
            if (AttackType == "Explosive")
            {
                for (; damage > 0; damage--)
                {
                    health--;
                    if (health <= 0) { break; }
                }
            }
        }
        if (enemyType == EnemyType.Ghost)
        {
            if (AttackType == "Normal")
            {
                for (; damage > 0; damage--)
                {
                    health--;
                    if (health <= 0) { break; }
                }
            }
        }

        if (health <= 0)
        {
            GameManager.Instance.Currency += worth;
            Destroy(gameObject);
            GameManager.Instance.enemyCount--;
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
