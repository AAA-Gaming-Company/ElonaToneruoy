using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FryingPan : MonoBehaviour
{
    public LayerMask playerLayer;
    public MMF_Player pickupFeedback;
    public Volume globalVolume;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().hasFryingPan = true;
            pickupFeedback.PlayFeedbacks();
        }
    }

    public void SetBlur()
    {
        if (globalVolume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField)) 
        {
            depthOfField.focusDistance.value = 1;
        }
    }
}
