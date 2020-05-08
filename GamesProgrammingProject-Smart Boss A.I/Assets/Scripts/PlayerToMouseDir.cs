using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToMouseDir : MonoBehaviour
{

    void Update()
    {
        PlayerToMouse();
    }

    void PlayerToMouse() //Sets the player facing the point of the mouse, important for aiming.
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction; //rotating to position
    }
}
