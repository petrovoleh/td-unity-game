using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostDebuff : Debuff
{
    private float slowingFactor;

    private bool applied;

    public FrostDebuff( float duration, float slowingFactor, Enemy target) : base(target, duration)
    {
        this.slowingFactor = slowingFactor;
    }
    public override void Update()
    {
       if(target != null)
        {
            if(!applied)
            {
                applied = true;
                target.Movespeed -= (target.MaxSpeed * slowingFactor) / 100 ;
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
