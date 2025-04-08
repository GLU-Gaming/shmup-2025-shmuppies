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

    private void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(currentRotation.x, -90, currentRotation.z);
    }
}