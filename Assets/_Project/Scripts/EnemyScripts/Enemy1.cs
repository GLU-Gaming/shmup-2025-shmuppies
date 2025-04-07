using UnityEngine;

public class Enemy1 : EnemyBase
{
    [SerializeField] public GameObject score;
    protected override void Start()
    {
        base.Start();
        speed = 3.5f; 
        xpDropped = 25f; // More XP

    }
}