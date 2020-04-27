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
    //private BaseEnemy BaseEnemyScript;

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

    /*
    //FeatureProgression
    //mini-boss1
    public int AmountOfShotsFiredMB1;
    public int AmountOfHealthRemainingMB1;
    //mini-boss2
    public int AmountOfShotsFiredMB2;
    public int AmountOfHealthRemainingMB2;
    */


    void Start()
    {
        HealthBar.value = Health;
        HealthText.text = Health.ToString();
        //BaseEnemyScript = GameObject.FindObjectOfType<BaseEnemy>();

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

        //if mini-boss1 is dead, advancelevel(); //This is done in the BaseEnemy script
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

    void AdvanceLevel()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           SceneManager.LoadScene("Level 2");
           Debug.Log("I pressed E" + "Loading Level 2 is working");
        }
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

    void Shoot()
    {
        Instantiate(LaserPrefab, FireLocation.position, FireLocation.rotation); //Spawning in the bullet. //GameObject Laser = 
        LaserPrefab.GetComponent<LaserCollisions>().Name = gameObject.name;
    }
}

//Future assets to use
//https://assetstore.unity.com/packages/2d/environments/animated-2d-coins-22097 - coins for drops