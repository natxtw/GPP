using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Core
    public Rigidbody2D PlayerRB;
    Vector2 PlayerMovementVector;
    private MiniBoss1Script MiniBoss1;

    //Stats
    public float Movespeed = 5.0f;
    public float Health = 100.0f;

    //UI
    public Slider HealthBar;

    void Start()
    {
        MiniBoss1 = GetComponent<MiniBoss1Script>();

    }

    void Update()
    {
        PlayerMovementVector.x = Input.GetAxisRaw("Horizontal");
        PlayerMovementVector.y = Input.GetAxisRaw("Vertical");

        HealthBar.value = Health;
    }

    void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + PlayerMovementVector * Movespeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision2D Working");
        if (col.gameObject.tag == ("Enemy"))
        {
            Health -= MiniBoss1.Damage;
            Debug.Log("I damaged the player");
            
        }
    }
}

//Future assets to use
//https://assetstore.unity.com/packages/2d/environments/animated-2d-coins-22097 - coins for drops
//https://assetstore.unity.com/packages/2d/environments/robot-shooting-game-sprite-free-93902 - boss 1 enemy assets