using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroTower : Tower
{
    public override Debuff GetDebuff()
    {
        return new NoDebuff(Target);
    }
}
