using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    public LayerMask playerLayer;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().hasFryingPan = true;
            Destroy(gameObject);
        }
    }
}
