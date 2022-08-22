using UnityEngine;

public class FlashlightSpin : MonoBehaviour
{
    private Camera camera;

    public void Start()
    {
        camera = Camera.main;
        RotateFlashlight();
    }

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
        {
            RotateFlashlight();
        }
    }

    private void RotateFlashlight()
    {
        Vector3 playerPos = transform.position;
        Vector3 cameraPos = camera.transform.position;
        cameraPos.z = playerPos.z;
        cameraPos.y -= 1.119f; //Camera offset from the middle

        Vector3 playerCameraDiff = (playerPos - cameraPos);

        float angle = AngleUtils.GetAngleFromVectorFloat(GetMouseDirection(playerPos) + playerCameraDiff);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private Vector3 GetMouseDirection(Vector3 player)
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = player.z; //Just to make sure that everything is all on the same z as the player

        return (targetPosition - player).normalized;
    }

}
