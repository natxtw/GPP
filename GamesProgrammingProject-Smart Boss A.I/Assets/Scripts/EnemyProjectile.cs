﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : BaseEnemy
{
    //EnemyShooting                 //player position if you want it to follow the player, target to the players previous position
    private float EnemySpeed;
    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        EnemySpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, EnemySpeed * Time.deltaTime); //Gets the position to shoot at
        
        if(transform.position.x == target.x && transform.position.y == target.y) //Destroys gameobject if it doesn't collide with anything
        {
            Destroy(gameObject);
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //can play some effects if i find some.
    }
    
}
