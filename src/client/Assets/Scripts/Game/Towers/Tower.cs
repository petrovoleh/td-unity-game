using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public abstract class Tower : MonoBehaviour
{
    
    [SerializeField]
    private string attackType;
    public string AttackType { 
        get 
        { 
            return attackType; 
        }
    }

    [SerializeField]
    private string towerType;
    public string TowerType
    {
        get
        {
            return towerType;
        }
    }

    public enum TowerTargetType
    {
        First, Last, Strongest, Weakest, Closest
    }
    public TowerTargetType towerTargetType;

    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private SpriteRenderer myRangeRenderer;

    public int Price { get; set; }

    [SerializeField]
    private string attackRangeType;

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    //Tower/Projectile Damage
    [SerializeField]
    private int damage;
    public  int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            this.damage = value;
        }
    }

    [SerializeField]
    private float debuffDuration;

    public float DebuffDuration
    {
        get
        {
            return debuffDuration;
        }
        set
        {
            this.debuffDuration = value;
        }
    }

    //Tower/Projectile SplashRange for damage
    [SerializeField]
    private int splashRange;
    public int SplashRange
    {
        get
        {
            return splashRange;
        }

        set
        {
            this.splashRange = value;
        }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    public TowerUpgrade[] Upgrades { get; protected set; }

    private Enemy target;

    public Enemy Target
    {
        get { return target; }
    }

    private Queue<Enemy> enemies = new Queue<Enemy>();

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;




    [SerializeField]
    private CircleCollider2D attackRadius;




    // Start is called before the first frame update
    void Start()
    {
         mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
         mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
    void Update()
    {
        
        Attack();
    }

    private void Attack()
    {

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        
        if (target == null && enemies.Count > 0)
        {
            target = enemies.Dequeue();

        }
        if(target != null)
        {
            
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }
        else if(enemies.Count > 0)
        {
            target = enemies.Dequeue();
        }
        if(target != null && !target.Alive )
        {
            target = null;
        }
    }

    private void Shoot()
    {
        if (attackRangeType == "Ranged")
        {
            Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

            projectile.transform.position = transform.position;

            projectile.Initialize(this);
        }
        else if(attackRangeType == "Melee")
        {
            myRangeRenderer.enabled = !myRangeRenderer.enabled;
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.TakeDamage(Damage, AttackType);
                    
                }
            }
            StartCoroutine(ExecuteAfterTime(0.2f));
            
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            enemies.Enqueue(other.GetComponent<Enemy>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            target = null;
        }
    }
    public abstract Debuff GetDebuff();

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        myRangeRenderer.enabled = !myRangeRenderer.enabled;
    }


   
}
