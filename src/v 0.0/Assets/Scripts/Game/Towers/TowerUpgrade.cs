using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade 
{
    public int Price { get; private set; }

    public int Damage { get; private set; }

    public float DebuffDuration { get; private set; }

    public float SlowFactor { get; private set; }

    public int BounceCount { get; private set; }

    public int TickTime { get; private set; }

    public float SplashRange { get; private set; }

    public float AttackCooldown { get; private set; }

    public TowerUpgrade(int price, int damage, float splashRange, float attackCooldown)
    {
        this.Damage = damage;
        this.SplashRange = splashRange;
        this.Price = price;
        this.AttackCooldown = attackCooldown;
    }
    public TowerUpgrade(int price, int damage, int bounceCount, float attackCooldown)
    {
        this.Damage = damage;
        this.AttackCooldown = attackCooldown;
        this.BounceCount = bounceCount;
        this.Price = price;
    }

    public TowerUpgrade(int price, int damage, int tickTime, float debuffduration, float slowfactor, float attackCooldown)
    {
        this.Damage = damage;
        this.DebuffDuration = debuffduration;
        this.SlowFactor = slowfactor;
        this.AttackCooldown = attackCooldown;
        this.Price = price;
        this.TickTime = tickTime;
    }
 
 
    
}
