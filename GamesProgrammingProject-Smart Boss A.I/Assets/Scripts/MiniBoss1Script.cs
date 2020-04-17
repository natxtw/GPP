using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniBoss1Script : BaseEnemy
{
    //Core
    private Shooting ShootingScript;
    public TextMeshProUGUI HealthText;
    public Slider HealthBar;


    //Feature
    public Transform MinionsSpawnLocation;
    public Transform MinionsSpawnLocationTwo;
    public GameObject MinionPrefab;
    public float MinionMaxHealth = 100.0f;
    public float MinionDamage = 5.0f;
    public float SpawnTimer = 2.0f;
    public bool StopSpawning = false;
    private int SpawnID = 0;

    //TODO:
    //Seperate Minions HP & Damage

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
        else
        {
            HealthBar.enabled = false; //doesn't work 
            //MiniBoss2isAlive = true;

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

    void MiniBossOneFeatures()
    {
      
        if (gameObject.name == "MiniBoss-1")
        {
            if (CurrentHealth <= MaxHealth / 4)//Feature 2
            {
                if (!StopSpawning)
                {
                    SpawnTimer -= Time.deltaTime;
                    if (SpawnTimer <= 0)
                    {
                        SpawnMinions(MinionsSpawnLocation);
                        SpawnMinions(MinionsSpawnLocationTwo);
                        StopSpawning = true;
                        StartCoroutine(ResetStopSpawning());
                    }
                }

            }
        }
        if (CurrentHealth <= MaxHealth / 2)//Feature 1
        {
            MovementSpeed = 6.5f;
        }


    }

    void SpawnMinions(Transform minionTransform)
    {
        GameObject cloneA =  Instantiate(MinionPrefab, minionTransform.position, minionTransform.rotation); //Spawning the minion
        cloneA.transform.localScale = new Vector3(2, 2, 1);
        SpawnID++;
        cloneA.name = "Minion " + SpawnID;
 
    }
    IEnumerator ResetStopSpawning()
    {
        yield return new WaitForSeconds(2);
        StopSpawning = false;
        SpawnTimer = 2f;
    }
}
