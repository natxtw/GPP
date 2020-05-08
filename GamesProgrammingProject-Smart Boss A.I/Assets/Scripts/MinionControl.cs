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
    void ApplyValues() //Applies stats
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

    private void FixedUpdate() //Follows the player
    {
        FollowPlayer(Movement, MovementSpeed);
    }
    private void Update() //If the boss is not alive, stop moving, you're not powered/controlled anymore.
    {
        if (MiniBoss1isAlive == true)
        {
            EnemyMovement();
        }
    }
    void OnCollisionEnter2D(Collision2D col) //Damage collider
    {
        if (col.gameObject.tag == "Bullet")
        {
            RecieveDamage(col.transform.GetComponent<LaserCollisions>().LaserDamage);

            Destroy(col.gameObject);
        }
    }
}
