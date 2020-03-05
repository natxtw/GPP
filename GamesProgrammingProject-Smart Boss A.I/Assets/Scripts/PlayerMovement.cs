using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    //Core
    public Rigidbody2D PlayerRB;
    Vector2 PlayerMovementVector;

    //Stats
    public float Movespeed = 5.0f;
    public float Health = 100.0f;

    //UI
    public Slider HealthBar;
    public TextMeshProUGUI HealthText;

    void Start()
    {
        HealthBar.value = Health;
        HealthText.text = Health.ToString();
    }

    void Update()
    {
        PlayerMovementVector.x = Input.GetAxisRaw("Horizontal");
        PlayerMovementVector.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + PlayerMovementVector * Movespeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            Health -= col.transform.GetComponent<BaseEnemy>().GetDamage();

            HealthBar.value = Health;
            HealthText.text = Health.ToString();

        }
    }
}

//Future assets to use
//https://assetstore.unity.com/packages/2d/environments/animated-2d-coins-22097 - coins for drops