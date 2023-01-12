using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireTower : Tower
{
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
        return new FireDebuff(TickDamage, TickTime, DebuffDuration, Target);
    }
}
