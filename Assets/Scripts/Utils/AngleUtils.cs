using UnityEngine;

public class AngleUtils
{
    public static Vector3 GetVector3FromAngle(float angle)
    {
        float angleRadius = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRadius), Mathf.Sin(angleRadius));
    }

    public static float GetAngleFromVectorFloat(Vector3 direction)
    {
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
