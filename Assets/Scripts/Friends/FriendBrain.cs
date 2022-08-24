using UnityEngine;
using Pathfinding;

public class FriendBrain : MonoBehaviour
{
    private Transform player;
    private FriendManager manager;
    private AIPath path;

    private void Start()
    {
        path = GetComponent<AIPath>();
        InvokeRepeating("SetDestination", 0.3f, 0.3f);
    }

    private void SetDestination()
    {
        path.destination = player.position + new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f));
    }

    public void SetupBrain(Transform player, FriendManager manager)
    {
        this.player = player;
        this.manager = manager;
    }

    public void GetEaten()
    {
        StartCoroutine(manager.FriendEaten(gameObject));
    }
}
