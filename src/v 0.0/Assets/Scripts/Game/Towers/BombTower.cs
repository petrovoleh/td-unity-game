using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Tower
{
    
    private void Start()
    {
        
    }
    public override Debuff GetDebuff()
    {
        return new NoDebuff(Target);
    }
}
