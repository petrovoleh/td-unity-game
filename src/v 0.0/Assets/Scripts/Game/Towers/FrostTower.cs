using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : Tower
{
    [SerializeField]
    private float slowingFactor;
    
    public override Debuff GetDebuff()
    {
        return new FrostDebuff(DebuffDuration, slowingFactor, Target);
    }
}
