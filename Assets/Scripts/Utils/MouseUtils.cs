using UnityEngine;

public class MouseUtils
{
    public static Vector3 GetMouseDirection(Vector3 player)
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = player.z; //Just to make sure that everything is all on the same z as the player

        return (targetPosition - player).normalized;
    }
}
