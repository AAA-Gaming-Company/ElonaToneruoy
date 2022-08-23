using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FriendBrain : MonoBehaviour
{
    public Transform player;
    AIPath path;

    public void Start()
    {
        path = GetComponent<AIPath>();
        InvokeRepeating("SetDestination", .3f, .3f);
    }
    void SetDestination()
    {
        path.destination = player.position + new Vector3(Random.Range(-4f, 4f), Random.Range(-2f, 2f));
    }
}
