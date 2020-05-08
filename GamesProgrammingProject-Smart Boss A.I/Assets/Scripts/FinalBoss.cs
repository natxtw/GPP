using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalBoss : BaseEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        ShootingScript = GetComponent<Shooting>();
        ApplyValues();
    }

    void ApplyValues()
    {
        MaxDamage = 5;
        Damage = MaxDamage;

        MaxRangedDamage = 5;
        RangedDamage = MaxRangedDamage;

        MaxHealth = 300;
        CurrentHealth = MaxHealth;

        MaxMovementSpeed = 2;
        MovementSpeed = MaxMovementSpeed;

        JustTeleported = false;
        FinalBossisAlive = true;

        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Movement = new Vector2();
        HealthText.text = CurrentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (FinalBossisAlive == true)
        {
            EnemyMovement();
            FinalBossFeatures();
            HealthBar.value = CurrentHealth;

        }
        else
        {
            HealthBar.enabled = false;

        }

    }

    private void FixedUpdate()
    {
        FollowPlayer(Movement, MovementSpeed);
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

    void FinalBossFeatures()
    {
        if (gameObject.name == "Final-Boss")
        {
            if (FinalBossisAlive == true)//Feature 1
            {
                if(AmountOfShotsFiredMB1Feature1 > AmountOfShotsFiredMB1Feature2)
                {
                    MinionFeature();
                    Debug.Log("Minions Should Spawn");
                }
                else
                {
                    MoveSpeedBoost();
                    Debug.Log("fast speed");
                }
            }

            if (CurrentHealth <= MaxHealth / 2)//Feature 2
            {
                if (AmountOfShotsFiredMB2Feature1 > AmountOfShotsFiredMB2Feature2)
                {
                    RangeAttacks();
                }
                else
                {
                    if (JustTeleported == false)
                    {
                        Teleportation();
                        StartCoroutine(TeleportingCD());
                        JustTeleported = true;
                    }
                }
            }
        }
    }
}
