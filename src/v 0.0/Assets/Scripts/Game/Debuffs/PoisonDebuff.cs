using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : Debuff
{
    
    private float slowingFactor;

    private bool applied;

    private float tickTime;

    private float timeSinceTick;

    private float tickDamage;

    public PoisonDebuff(float tickDamage, float tickTime, float duration, float slowingFactor, Enemy target) : base(target, duration)
    {
        this.slowingFactor = slowingFactor;
        this.tickDamage = tickDamage;
        this.tickTime = tickTime;
    }

    public override void Update()
    {
        if (target != null)
        {
            if(!applied)
            {
                applied = true;
                target.Movespeed -= (target.MaxSpeed * slowingFactor) / 100;


            }
            
            timeSinceTick += Time.deltaTime;

            if (timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;

                target.TakeDamage(tickDamage);
            }
        }

        base.Update();
    }

    public override void Remove()
    {
        target.Movespeed = target.MaxSpeed;
        base.Remove();
    }
}
