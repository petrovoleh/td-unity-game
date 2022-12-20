using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Enemy target;

    private Tower parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public void Initialize(Tower parent)
    {
        this.target = parent.Target;
        this.parent = parent;
    }
    public void MoveToTarget()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * parent.ProjectileSpeed);

            Vector2 dir = target.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if(target == null)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDebuff()
    {
        target.AddDebuff(parent.GetDebuff());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {

            if(target.gameObject == other.gameObject && parent.SplashRange == 0)
            {
                
                target.TakeDamage(parent.Damage, parent.AttackType);
                Destroy(gameObject);
                ApplyDebuff();
            }
            if (parent.SplashRange > 0)
            {

                var hitColliders = Physics2D.OverlapCircleAll(transform.position, parent.SplashRange);
                foreach(var hitCollider in hitColliders)
                {
                    var enemy = hitCollider.GetComponent<Enemy>();
                    if(enemy)
                    {
                        enemy.TakeDamage(parent.Damage, parent.AttackType);
                        Destroy(gameObject);
                        ApplyDebuff();
                    }
                }
      
            }

        }
    }

    
}
