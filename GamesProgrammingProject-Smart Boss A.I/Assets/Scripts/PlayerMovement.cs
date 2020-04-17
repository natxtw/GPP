using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    //Core
    public Rigidbody2D PlayerRB;
    Vector2 PlayerMovementVector;
    public Camera Cam;
    Vector2 MousePos;
    private EnemyProjectile EnemyProj;

    //Stats
    public float Movespeed = 5.0f;
    public float Health = 100.0f;
    private float NegativeHealth = 0.0f;

    //UI
    public Slider HealthBar;
    public TextMeshProUGUI HealthText;

    void Start()
    {
        HealthBar.value = Health;
        HealthText.text = Health.ToString();
        EnemyProj = GetComponent<EnemyProjectile>();
    }

    void Update()
    {
        PlayerMovementVector.x = Input.GetAxisRaw("Horizontal");
        PlayerMovementVector.y = Input.GetAxisRaw("Vertical");

        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);

        //if() // if MiniBoss1isAlive = fasle ... if key pressed = E ... load level 2 scene

        if(Health <= NegativeHealth)
        {
            PlayerDeath();
        }
    }

    void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + PlayerMovementVector * Movespeed * Time.fixedDeltaTime);
        Vector2 LookDir = MousePos - PlayerRB.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        PlayerRB.rotation = angle;
    }

    void PlayerDeath()
    {
        //Debug.Log("This should not appaer");
        SceneManager.LoadScene("Lose Screen");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Health -= col.transform.GetComponent<BaseEnemy>().GetDamage();

            HealthBar.value = Health;
            HealthText.text = Health.ToString();
            //Debug.Log("I should have lost health");
        }

        if (col.gameObject.tag == "123")
        {
            Health -= col.transform.GetComponent<BaseEnemy>().GetRangeDamage();

            HealthBar.value = Health;
            HealthText.text = Health.ToString();
            Debug.Log("RangeDamage should have applied");
        }

    }
}

//Future assets to use
//https://assetstore.unity.com/packages/2d/environments/animated-2d-coins-22097 - coins for drops