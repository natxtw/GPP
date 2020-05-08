﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingNo2 : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public float bulletforce = 20.0f;

    //unused script, an attempt at fixing a bug related to the shooting mechanic
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletforce, ForceMode2D.Impulse);
    }
}
