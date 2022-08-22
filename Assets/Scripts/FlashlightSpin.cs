using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSpin : MonoBehaviour
{
    Camera cam;

    public void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos =cam.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
