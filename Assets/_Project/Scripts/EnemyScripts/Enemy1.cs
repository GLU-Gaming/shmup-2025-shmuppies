using UnityEngine;

public class Enemy1 : EnemyBase
{
    protected override void Start()
    {
        base.Start();
        speed = 3.5f; 
        xpDropped = 25f; // More XP
    }
}