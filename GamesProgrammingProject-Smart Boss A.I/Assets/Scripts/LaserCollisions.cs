using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollisions : MonoBehaviour
{
    //Core
    Transform Parent;

    //Stats
    public float LaserSpeed = 20.0f;
    public float LaserDamage = 5.0f;
    void Start()
    {
        Parent = GameObject.Find("Player").transform;
        GetComponent<Rigidbody2D>().AddForce(Parent.GetChild(0).up * LaserSpeed, ForceMode2D.Impulse); //Adding force to the laser.
    
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //can play some effects if i find some.
    }


 


}
