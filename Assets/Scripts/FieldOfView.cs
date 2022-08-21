using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject player;
    public Camera camera;

    public LayerMask layerMask;

    public float fov = 90f;
    public int rayCount = 50;
    public float viewDistance = 10f;

    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate() //Run after everything else to use the optimal positioning
    {
        Vector3 cameraPos = camera.transform.position;
        cameraPos.z = player.transform.position.z;
        cameraPos.y -= 0.96f; //PLAYER COLLIDER HEIGHT
#warning FIX THIS NOW
        Vector3 playerCameraDiff = (player.transform.position - cameraPos);

        Vector3 origin = player.transform.position;
        float angle = AngleUtils.GetAngleFromVectorFloat(MouseUtils.GetMouseDirection(player.transform.position) + playerCameraDiff) + (fov / 2f);
        float angleIncrease = fov / rayCount;

        Vector3[] vertecies = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertecies.Length];
        int[] triangles = new int[rayCount * 3];

        vertecies[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit2D raycastHit = Physics2D.Raycast(origin, AngleUtils.GetVector3FromAngle(angle), viewDistance, layerMask);
            if (raycastHit.collider == null)
            {
                vertex = origin + AngleUtils.GetVector3FromAngle(angle) * viewDistance;
            } else
            {
                vertex = raycastHit.point;
            }

            vertecies[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertecies;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
