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
    protected Vector2 Movement;
    public bool MiniBoss1isAlive = true;
    protected Rigidbody2D rb;
    public Transform Player;

    void Start()
    {
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
      
 
    }
    public void SetValues()
    {
        Player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
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

}
