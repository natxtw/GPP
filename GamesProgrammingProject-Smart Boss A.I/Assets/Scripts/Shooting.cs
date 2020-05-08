using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //Script moved into player.
    public Transform FireLocation;
    public GameObject LaserPrefab;

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
        Instantiate(LaserPrefab, FireLocation.position, FireLocation.rotation); //Spawning in the bullet. //GameObject Laser = 
        LaserPrefab.GetComponent<LaserCollisions>().Name = gameObject.name;
    }
}
