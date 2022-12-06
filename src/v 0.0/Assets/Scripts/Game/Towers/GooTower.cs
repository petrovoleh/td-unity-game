using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooTower : Tower
{
    [SerializeField]
    private float slowingFactor;

    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get
        {
            return tickTime;
        }
        set
        {
            this.tickTime = value;
        }
    }

    public float TickDamage
    {
        get
        {
            return tickDamage;
        }
        set
        {
            this.tickDamage = value;
        }
    }
    
    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(TickDamage, TickTime, DebuffDuration, slowingFactor, Target);
    }

}
