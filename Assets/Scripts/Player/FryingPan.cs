using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FryingPan : MonoBehaviour
{
    public LayerMask playerLayer;
    public MMF_Player pickupFeedback;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().hasFryingPan = true;
            pickupFeedback.PlayFeedbacks();
        }
    }
}
