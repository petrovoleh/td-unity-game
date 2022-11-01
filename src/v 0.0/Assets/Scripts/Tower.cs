using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    private Enemy target;

    public Enemy Target
    {
        get { return target; }
    }

    public int Damage
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

    private Queue<Enemy> enemies = new Queue<Enemy>();

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;

    /* private SpriteRenderer mySpriteRenderer;
     // Start is called before the first frame update
     void Start()
     {
         mySpriteRenderer = GetComponent<SpriteRenderer>();
     }

     // Update is called once per frame
     void Update()
     {

     }

     public void Select()
     {
         mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
     }*/
    void Update()
    {
        Attack();
        //Debug.Log(target);
    }



    private void Attack()
    {

        if(!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if(target == null && enemies.Count > 0)
        {
            target = enemies.Dequeue();

        }
        if(target != null)
        {
            if(canAttack)
            {
                Shoot();

                canAttack = false;
            }
        }
    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);

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
}
