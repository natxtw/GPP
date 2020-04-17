using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
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


    protected Vector2 Movement;
    public bool MiniBoss1isAlive = true;
    public bool MiniBoss2isAlive = true;
    protected Rigidbody2D rb;
    public Transform Player;

    public GameObject Projectile;

    //Shooting Feature
    private float TimeBetweenShots;
    public float StartTimeBetweenShots;
    public Transform ShootingPos;

    //Teleporting
    [SerializeField] protected float xMaxRange;
    [SerializeField] protected float xMinRange;
    [SerializeField] protected float yMaxRange;
    [SerializeField] protected float yMinRange;
    [SerializeField] protected bool JustTeleported;

    void Start()
    {
        SetValues();
    }

    void Update()
    {
      //Nothing Should ever be called in here, this is the base enemy class.
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

    public IEnumerator TeleportingCD()
    {
        yield return new WaitForSeconds(2);
        JustTeleported = false;
    }

}
