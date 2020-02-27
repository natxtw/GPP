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

    //Stats
    public float MoveSpeed = 2.5f;
    public float Health = 100.0f;
    public float Damage = 20.0f;


    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
        ShootingScript = GetComponent<Shooting>();
    }
   
    void Update()
    {
        Vector3 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RB.rotation = angle;
        direction.Normalize();
        Movement = direction;

        if(Health <= Health / 2)
        {
            MoveSpeed = 7.5f;
        }

        if(Health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        FollowPlayer(Movement);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void FollowPlayer(Vector2 direction)
    {
        RB.MovePosition((Vector2)transform.position + (direction * MoveSpeed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision2D Working");
        if (col.gameObject.tag == ("Bullet"))
        {
            Health -= ShootingScript.LaserDamage;
            Debug.Log("I damaged the boss");
            Destroy(col.gameObject);
        }
    }

}
