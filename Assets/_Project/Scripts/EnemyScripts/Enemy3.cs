using UnityEngine;

public class Enemy3 : EnemyBase
{
    [SerializeField] public GameObject score;
    protected override void Start()
    {
        base.Start();
        speed = 3.5f;
        xpDropped = 75f; // More XP
        
    }
}