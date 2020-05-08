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

    //Stats
    public float Movespeed = 5.0f;
    public float Health = 100.0f;
    private float NegativeHealth = 0.0f;
    private float Score = 0;

    //Shooting
    public Transform FireLocation;
    public GameObject LaserPrefab;

    //UI
    public Slider HealthBar;
    public TextMeshProUGUI HealthText;


    void Start()//Linking to the UI
    {
        HealthBar.value = Health;
        HealthText.text = Health.ToString();

    }


    void Update()
    {
        PlayerMovementVector.x = Input.GetAxisRaw("Horizontal");
        PlayerMovementVector.y = Input.GetAxisRaw("Vertical");

        MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) //or GetButtonDown("Fire1")
        {
            Shoot();
        }

        if (Health <= NegativeHealth)
        {
            PlayerDeath();
        }

    }

    void FixedUpdate()//rotating the player based of the direction the mouse is looking
    {
        PlayerRB.MovePosition(PlayerRB.position + PlayerMovementVector * Movespeed * Time.fixedDeltaTime);
        Vector2 LookDir = MousePos - PlayerRB.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        PlayerRB.rotation = angle;
    }

    void PlayerDeath()//Player dies
    {
        SceneManager.LoadScene("Lose Screen");
    }

    void AdvanceLevel()//Test feature, can remove later, if need be
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           SceneManager.LoadScene("Level 2");
           Debug.Log("I pressed E" + "Loading Level 2 is working");
        }
    }

    void OnCollisionEnter2D(Collision2D col)//Collider to die
    {
        if (col.gameObject.tag == "Enemy")
        {
            Health -= col.transform.GetComponent<BaseEnemy>().GetDamage();

            HealthBar.value = Health;
            HealthText.text = Health.ToString();
            //Debug.Log("I should have lost health");
        }

        if (col.gameObject.tag == "EnemyProjectile")
        {
            Health -= (col.transform.GetComponent<BaseEnemy>().GetRangeDamage() + 5);
            HealthBar.value = Health;
            HealthText.text = Health.ToString();
            Debug.Log("I should have lost health");
        }

    }

    void Shoot()//Shoot script that was moved into the player to fix a bug
    {
        Instantiate(LaserPrefab, FireLocation.position, FireLocation.rotation); //Spawning in the bullet. //GameObject Laser = 
        LaserPrefab.GetComponent<LaserCollisions>().Name = gameObject.name;
    }
}

//Future assets to use
//https://assetstore.unity.com/packages/2d/environments/animated-2d-coins-22097 - coins for drops