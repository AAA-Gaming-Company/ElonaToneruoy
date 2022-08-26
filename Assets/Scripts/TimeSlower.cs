using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class TimeSlower : MonoBehaviour
{
    public MMTimeManager timeManager;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            timeManager.SetTimescaleTo(0.5f);
            Debug.Log("Slowed");
        }else
        {
            timeManager.SetTimescaleTo(1f);
        }
    }
}
