using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseEnemy : MonoBehaviour
{
    //UI
    protected Shooting ShootingScript;
    public TextMeshProUGUI HealthText;
    public Slider HealthBar;

    //Stats
    [SerializeField] protected float MaxHealth;
    [SerializeField] protected float CurrentHealth;
    [SerializeField] protected float MaxDamage;
    [SerializeField] protected float Damage;
    [SerializeField] protected float MaxMovementSpeed;
    [SerializeField] protected float MovementSpeed;

    [SerializeField] protected float RetretingDistance;
    [SerializeField] protected float StoppingDistance;
    [SerializeField] protected float MaxRangedDamage;
    [SerializeField] protected float RangedDamage;


    [SerializeField] protected Vector2 Movement;
    [SerializeField] public bool MiniBoss1isAlive = true;
    [SerializeField] public bool MiniBoss2isAlive = false;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] public Transform Player;

    public GameObject Projectile;

    //Shooting Feature //Collisions between the projectile and the player interacting is currently broken
    [SerializeField] protected float TimeBetweenShots;
    [SerializeField] protected float StartTimeBetweenShots;
    [SerializeField] public Transform ShootingPos;

    //Teleporting
    [SerializeField] protected float xMaxRange;
    [SerializeField] protected float xMinRange;
    [SerializeField] protected float yMaxRange;
    [SerializeField] protected float yMinRange;
    [SerializeField] protected bool JustTeleported;

    //Minions
    //TODO: Seperate Minions HP & Damage
    public Transform MinionsSpawnLocation;
    public Transform MinionsSpawnLocationTwo;
    public GameObject MinionPrefab;
    public float MinionMaxHealth = 100.0f;
    public float MinionDamage = 5.0f;
    public float SpawnTimer = 2.0f;
    public bool StopSpawning = false;
    protected int SpawnID = 0;

    private Vector3 SpawnPoint;

    //FeatureProgression
    //mini-boss1
    public int AmountOfShotsFiredMB1;
    public int AmountOfShotsFiredMB1Feature1;
    public int AmountOfShotsFiredMB1Feature2;
    public int AmountOfHealthRemainingMB1;
    //mini-boss2
    public int AmountOfShotsFiredMB2;
    public int AmountOfShotsFiredMB2Feature1;
    public int AmountOfShotsFiredMB2Feature2;
    public int AmountOfHealthRemainingMB2;
    

    void Start()
    {
        SetValues();
    }

    void Update()
    {
       
    }

    public void SetValues()
    {
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        TimeBetweenShots = StartTimeBetweenShots;
    }

 
    protected void EnemyMovement()
    {
        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        Movement = direction;
    }
    protected void FollowPlayer(Vector2 direction , float moveSpeed)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    
    }

    protected void RangedCombatMovement()
    {
        if (Vector2.Distance(transform.position, Player.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, MovementSpeed * Time.deltaTime);
        }
        else if(Vector2.Distance(transform.position, Player.position) < StoppingDistance && Vector2.Distance(transform.position, Player.position) > RetretingDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, Player.position) < RetretingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -MovementSpeed * Time.deltaTime);
        }
    }
    protected void RecieveDamage(float DamageTaken)
    {
        CurrentHealth -= DamageTaken;
        if(CurrentHealth <= 0)
        {
            Destroy(gameObject);
            if (gameObject.name == "MiniBoss-1")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (gameObject.name == "MiniBoss-2")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    protected void DealDamage(float PlayerHealth)
    {
        PlayerHealth -= Damage;
  
    }
    public float GetDamage()
    {
        return Damage;
    }

    public float GetRangeDamage()
    {
        return RangedDamage;
    }

    public void RangeAttacks()
    {
        if (TimeBetweenShots <= 0)
        {
            Instantiate(Projectile, ShootingPos.position, Quaternion.identity);
            TimeBetweenShots = StartTimeBetweenShots;
        }
        else
        {
            TimeBetweenShots -= Time.deltaTime;
        }
    }

    public void Teleportation()
    {
        transform.position = new Vector2(Random.Range(xMinRange, xMaxRange), Random.Range(yMinRange, yMaxRange));
    }

    public void MoveSpeedBoost()
    {
        MovementSpeed = 6.5f;
    }

    public IEnumerator TeleportingCD()
    {
        yield return new WaitForSeconds(2);
        JustTeleported = false;
    }

    public void MinionFeature()
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

    public void SpawnMinions(Transform minionTransform)
    {
        GameObject cloneA = Instantiate(MinionPrefab, minionTransform.position, minionTransform.rotation); //Spawning the minion
        cloneA.transform.localScale = new Vector3(2, 2, 1);
        SpawnID++;
        cloneA.name = "Minion " + SpawnID;

    }

    public IEnumerator ResetStopSpawning()
    {
        yield return new WaitForSeconds(2);
        StopSpawning = false;
        SpawnTimer = 2f;
    }

    public void ProgressionPlayerTracking()
    {
        Debug.Log(CurrentHealth);
        if (MiniBoss1isAlive == true)
        {
            MiniBoss2isAlive = false;
            //Debug.Log("Miniboss1 is alive should be true " + " MiniBoss1isAlive = " + MiniBoss1isAlive); //Debug.Log("Miniboss2 is alive should be false " + " MiniBoss2isAlive = " + MiniBoss2isAlive);

            if (CurrentHealth <= MaxHealth)//Feature 1
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //AmountOfShotsFiredMB1Feature1 = AmountOfShotsFiredMB1Feature1 + 10; //Alternatively int++ and then create a multiplyer later on.
                    AmountOfShotsFiredMB1Feature1++;
                    //Debug.Log("Feature1 Score: " + AmountOfShotsFiredMB1Feature1); //already works
                    Debug.Log(CurrentHealth);
                }
            }

            if (CurrentHealth <= MaxHealth / 2)//Feature 2
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AmountOfShotsFiredMB1Feature2++;
                    AmountOfShotsFiredMB1Feature1--;
                    //Debug.Log("Feature2 Score: " + AmountOfHealthRemainingMB2);
                }
            }
        }
    }

    public void TrackingAddition()
    {

    }
}
