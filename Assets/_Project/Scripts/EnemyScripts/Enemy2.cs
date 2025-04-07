using UnityEngine;

public class Enemy2 : EnemyBase
{
    [SerializeField] public GameObject score;
    protected override void Start()
    {
        base.Start();
        speed = 5f; // Faster enemy
        xpDropped = 30f; // More XP
    }
}
