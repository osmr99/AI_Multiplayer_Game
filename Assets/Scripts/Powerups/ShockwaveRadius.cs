using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveRadius : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name != "Dot(Clone)")
        {
            collision.transform.position = transform.position;
        }
    }
}
