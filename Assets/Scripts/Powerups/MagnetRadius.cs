using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRadius : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float speed;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Dot>() != null)
        {
            collision.GetComponent<Dot>().targetPlayer = transform.parent;
        }
    }

    private void Update()
    {
        
    }
}
