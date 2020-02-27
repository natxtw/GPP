using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollisions : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //can play some effects if i find some.
    }


 


}
