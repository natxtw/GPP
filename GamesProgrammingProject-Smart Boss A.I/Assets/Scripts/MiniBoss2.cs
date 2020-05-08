using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MiniBoss2 : BaseEnemy
{
    void Start()
    {
        ShootingScript = GetComponent<Shooting>();
        ApplyValues();
    }

    void Update()
    {

        if (MiniBoss2isAlive == true)
        {
            EnemyMovement();
            MiniBoss2Features();
            HealthBar.value = CurrentHealth;

        }
        else
        {
            HealthBar.enabled = false; 

        }

    }

    private void FixedUpdate()
    {
        ///FollowPlayer(Movement, MovementSpeed);
        RangedCombatMovement();
    }

    void ApplyValues()
    {
        MaxDamage = 5;
        Damage = MaxDamage;

        MaxRangedDamage = 5;
        RangedDamage = MaxRangedDamage;

        MaxHealth = 100;
        CurrentHealth = MaxHealth;

        MaxMovementSpeed = 2;
        MovementSpeed = MaxMovementSpeed;

        JustTeleported = false;

        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Movement = new Vector2();
        HealthText.text = CurrentHealth.ToString();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            RecieveDamage(col.transform.GetComponent<LaserCollisions>().LaserDamage);
            HealthText.text = CurrentHealth.ToString();
            Destroy(col.gameObject);
        }
    }

    void MiniBoss2Features()
    {
        if (gameObject.name == "Mini-Boss2")
        {
            if (MiniBoss2isAlive == true)
            {
                RangeAttacks();

                if (Input.GetMouseButtonDown(0))
                {
                    AmountOfShotsFiredMB2Feature1++;
                    //Debug.Log("Feature1 Score: " + AmountOfShotsFiredMB1Feature1); //already works
                }

                if (CurrentHealth <= MaxHealth / 2)
                {
                    if (JustTeleported == false)
                    {
                        Teleportation();
                        StartCoroutine(TeleportingCD());
                        JustTeleported = true;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        AmountOfShotsFiredMB2Feature1--;
                        AmountOfShotsFiredMB2Feature2++;
                        //Debug.Log("Feature1 Score: " + AmountOfShotsFiredMB1Feature1); //already works
                    }
                }
            }
        }
    }
}
