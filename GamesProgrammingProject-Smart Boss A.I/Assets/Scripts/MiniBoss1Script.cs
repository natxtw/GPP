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
        ApplyValues(); //Setting values on start
    }

    void ApplyValues()
    {
        MaxDamage = 5;
        Damage = MaxDamage;

        MaxHealth = 100;
        CurrentHealth = MaxHealth;

        MaxMovementSpeed = 2;
        MovementSpeed = MaxMovementSpeed;

        Player = GameObject.Find("Player").transform; //Finds the player
        rb = GetComponent<Rigidbody2D>();
        Movement = new Vector2();
        HealthText.text = CurrentHealth.ToString(); //Links to the UI
    }
    void Update()
    {
        if (MiniBoss1isAlive == true) //Starts the miniboss
        {
            MiniBossOneFeatures();
            EnemyMovement();
            HealthBar.value = CurrentHealth; //Links to the onscreen UI
            
        }

        if(MiniBoss1isAlive == false)
        {
            HealthBar.enabled = false;            
        }

        //Debug.Log(AmountOfShotsFiredMB1Feature1); //Scoring tests
        //Debug.Log(AmountOfShotsFiredMB1Feature2); //Scoring tests
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

    void MiniBossOneFeatures() //Miniboss controller, controlls movement & calls function for features
    {
        if (gameObject.name == "MiniBoss-1")
        {
            if (CurrentHealth <= MaxHealth)//Feature 1
            {
                MinionFeature();

                if (Input.GetMouseButtonDown(0)) //Scoring system
                {
                    AmountOfShotsFiredMB1Feature1++;
                    //Debug.Log("Feature1 Score: " + AmountOfShotsFiredMB1Feature1); 
                }
            }

            if (CurrentHealth <= MaxHealth / 2)//Feature 2
            {
                MoveSpeedBoost();

                if (Input.GetMouseButtonDown(0)) //Scoring system
                {
                    AmountOfShotsFiredMB1Feature2++;
                    //Debug.Log("Feature2 Score: " + AmountOfShotsFiredMB1Feature2); 
                }
            }
        }
    }

}
