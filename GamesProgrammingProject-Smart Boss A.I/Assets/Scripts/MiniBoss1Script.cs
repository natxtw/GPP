using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss1Script : MonoBehaviour
{
    //Core
    public Transform Player;
    Rigidbody2D RB;
    private Vector2 Movement;
    private Shooting ShootingScript;
    public bool MiniBoss1isAlive = true;

    //Stats
    public float MoveSpeed = 1.5f;
    public float MaxHealth = 100.0f;
    public float Health;
    public float Damage = 20.0f;

    //Feature
    public Transform MinionsSpawnLocation;
    public Transform MinionsSpawnLocationTwo;
    public GameObject MinionPrefab;
    public float MinionMaxHealth = 100.0f;
    public float MinionDamage = 5.0f;
    public float SpawnTimer = 2.0f;
    public bool StopSpawning = false;
    private int SpawnID = 0;
    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
        ShootingScript = GetComponent<Shooting>();
        Health = MaxHealth;
        Player = GameObject.Find("Player").transform;
 
    }

    void Update()
    {
        if (MiniBoss1isAlive == true)
        {
            MiniBossOneFeatures();
        }
    }

    private void FixedUpdate()
    {
        FollowPlayer(Movement);
       
    }

    void Die()
    {
        Destroy(gameObject);
        MiniBoss1isAlive = false;
    }

    void FollowPlayer(Vector2 direction)
    {
        RB.MovePosition((Vector2)transform.position + (direction * MoveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Health -= col.transform.GetComponent<LaserCollisions>().LaserDamage;
            Destroy(col.gameObject);
        }
    }

    void MiniBossOneFeatures()
    {
        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RB.rotation = angle;
        direction.Normalize();
        Movement = direction;
        if (gameObject.name == "MiniBoss-1")
        {
            if (Health <= MaxHealth / 4)//Feature 2
            {
                if (!StopSpawning)
                {
                    SpawnTimer -= Time.deltaTime;
                    if (SpawnTimer <= 0)
                    {
                        SpawnMinions(MinionsSpawnLocation);

                        StopSpawning = true;
                        StartCoroutine(ResetStopSpawning());
                    }
                }

            }
        }
        if (Health <= MaxHealth / 2)//Feature 1
        {
            MoveSpeed = 2.5f;
        }
        if (Health <= 0)
        {
            Die();
        }

    }

    void SpawnMinions(Transform minionTransform)
    {
        GameObject cloneA =  Instantiate(gameObject, minionTransform.position, minionTransform.rotation); //Spawning the minion
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
