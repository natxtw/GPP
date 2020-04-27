using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MiniBoss1Script : BaseEnemy
{
    void Start()
    {
        ShootingScript = GetComponent<Shooting>();
        ApplyValues();
    }

    void ApplyValues()
    {
        MaxDamage = 5;
        Damage = MaxDamage;

        MaxHealth = 100;
        CurrentHealth = MaxHealth;

        MaxMovementSpeed = 2;
        MovementSpeed = MaxMovementSpeed;

        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        Movement = new Vector2();
        HealthText.text = CurrentHealth.ToString();
    }
    void Update()
    {
        if (MiniBoss1isAlive == true)
        {
            MiniBossOneFeatures();

            EnemyMovement();
            HealthBar.value = CurrentHealth;
            
        }

        if(MiniBoss1isAlive == false)
        {
            HealthBar.enabled = false; //doesn't work //Does it really matter?
            Debug.Log("I Should not show");
        }

        Debug.Log(AmountOfShotsFiredMB1Feature1);
        Debug.Log(AmountOfShotsFiredMB1Feature2);
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

    void MiniBossOneFeatures()
    {
        if (gameObject.name == "MiniBoss-1")
        {
            if (CurrentHealth <= MaxHealth)//Feature 1
            {
                MinionFeature();

                if (Input.GetMouseButtonDown(0))
                {
                    AmountOfShotsFiredMB1Feature1++;
                    Debug.Log("Feature1 Score: " + AmountOfShotsFiredMB1Feature1); //already works
                }
            }

            if (CurrentHealth <= MaxHealth / 2)//Feature 2
            {
                MoveSpeedBoost();

                if (Input.GetMouseButtonDown(0))
                {
                    AmountOfShotsFiredMB1Feature2++;
                    Debug.Log("Feature2 Score: " + AmountOfShotsFiredMB1Feature2); //already works
                }
            }
        }
    }

}
