using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBrain : MonoBehaviour
{
    AIDestinationSetter aIDestinationSetter;
    AIPath path;
    bool canAttack = true;
    public LayerMask friendMask;

    public void Start()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        InvokeRepeating("ScanForTargets", 1f, 1f);
        InvokeRepeating("Attack", 1f, 1f);
        path = GetComponent<AIPath>();
    }

    public void ScanForTargets()
    {
        Collider2D[] friends = Physics2D.OverlapCircleAll(transform.position, 200, friendMask);

        Debug.Log("Scanned");

        if (friends.Length != 0)
        {

            Debug.Log("Found");
            foreach(Collider2D friendCollider in friends)
            {
                if (aIDestinationSetter.target)
                {
                    if (Vector3.Distance(friendCollider.transform.position, transform.position) > Vector3.Distance(aIDestinationSetter.target.position, transform.position))
                    {
                        aIDestinationSetter.target = friendCollider.transform;
                    }
                }
                else
                {
                    aIDestinationSetter.target = friendCollider.transform;
                }
            }
        }
    }

    void Attack()
    {
        Collider2D friend = Physics2D.OverlapCircle(transform.position, 1, friendMask);

        if (friend && canAttack)
        {
            Kidnap(friend.gameObject);
        }
    }

    void Kidnap(GameObject friend)
    {
        Destroy(friend);
        StartCoroutine(Rest());
    }


    IEnumerator Rest()
    {
        path.canMove = false;
        canAttack = false;
        yield return new WaitForSeconds(12);
        canAttack = true;
        path.canMove = true;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
