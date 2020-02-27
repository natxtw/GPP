using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
    public GameObject PlayerToFollow;
    public Vector3 offset = new Vector3(0, 0, 1);
    void FixedUpdate()
    {
        if(PlayerToFollow)
        {
            transform.position = new Vector3(
                PlayerToFollow.transform.position.x + offset.x,
                PlayerToFollow.transform.position.y + offset.y,
                PlayerToFollow.transform.position.z + offset.z);
        }
    }
}
