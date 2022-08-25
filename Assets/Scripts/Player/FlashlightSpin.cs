using UnityEngine;

public class FlashlightSpin : MonoBehaviour
{
    private Camera cam;

    public void Start()
    {
        cam = Camera.main;
        RotateFlashlight();
    }

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0 && Time.timeScale != 0)
        {
            RotateFlashlight();
        }
    }

    private void RotateFlashlight()
    {
        Vector3 playerPos = transform.position;
        Vector3 cameraPos = cam.transform.position;
        cameraPos.z = playerPos.z;
        cameraPos.y -= 1.119f; //Camera offset from the middle

        Vector3 playerCameraDiff = (playerPos - cameraPos);

        float angle = AngleUtils.GetAngleFromVectorFloat(GetMouseDirection(playerPos) + playerCameraDiff);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private Vector3 GetMouseDirection(Vector3 player)
    {
        Vector3 targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = player.z; //Just to make sure that everything is all on the same z as the player

        return (targetPosition - player).normalized;
    }
}
