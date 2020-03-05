using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionControl : BaseEnemy
{
    void Start()
    {
        SetValues();
        ApplyValues();
    }
    void ApplyValues()
    {
        MaxDamage = 5;
        Damage = MaxDamage;
        MaxHealth = 25;
        CurrentHealth = MaxHealth;
        MaxMovementSpeed = 2;
        MovementSpeed = MaxMovementSpeed;
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Movement = new Vector2();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        FollowPlayer(Movement, MovementSpeed);
    }
    private void Update()
    {
        if (MiniBoss1isAlive == true)
        {
            EnemyMovement();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            RecieveDamage(col.transform.GetComponent<LaserCollisions>().LaserDamage);

            Destroy(col.gameObject);
        }
    }
}
