using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FireLocation;
    public GameObject LaserPrefab;
    public float LaserSpeed = 20.0f;
    public float LaserDamage = 20.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //or GetButtonDown("Fire1")
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject Laser = Instantiate(LaserPrefab, FireLocation.position, FireLocation.rotation); //Spawning in the bullet.
        Rigidbody2D LaserRb = Laser.GetComponent<Rigidbody2D>(); //Creating the Rigidbody.
        LaserRb.AddForce(FireLocation.up * LaserSpeed, ForceMode2D.Impulse); //Adding force to the laser.
    }
}
