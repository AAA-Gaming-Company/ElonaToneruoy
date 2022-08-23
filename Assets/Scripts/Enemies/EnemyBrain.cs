using System.Collections;
using UnityEngine;
using Pathfinding;
using MoreMountains.Feedbacks;

public class EnemyBrain : MonoBehaviour
{
    public LayerMask friendMask;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public MMF_Player eatFeedback;
    public float attackRadius = 1;

    private AIDestinationSetter aIDestinationSetter;
    private AIPath path;
    private bool canAttack = true;

    public void Start()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        
        InvokeRepeating("ScanForTargets", 1f, 1f);
        InvokeRepeating("Attack", 1f, 1f);
        StartCoroutine(AnimateWalk());
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

    IEnumerator AnimateWalk()
    {
        Debug.Log("Coroutine run");
        Vector3 oldPos = transform.position;

        yield return new WaitForSeconds(0.5f);
        if (transform.position.x > 0)
        {
            if (transform.position.x - oldPos.x > 0)
            {
                Debug.Log("Right");
                animator.SetBool("WalkSide", true);
                spriteRenderer.flipX = false;
            } else if (transform.position.x - oldPos.x < 0)
            {
                Debug.Log("Left");
                animator.SetBool("WalkSide", true);
                spriteRenderer.flipX = true;
            }
        } else if (transform.position.x < 0)
        {
            if (transform.position.x - oldPos.x > 0)
            {
                Debug.Log("Right");
                animator.SetBool("WalkSide", true);
                spriteRenderer.flipX = false;
            } else if (transform.position.x - oldPos.x < 0)
            {
                Debug.Log("Left");
                animator.SetBool("WalkSide", true);
                spriteRenderer.flipX = true;
            }
        }
    

        StartCoroutine(AnimateWalk());

    }


    void Attack()
    {
        Collider2D friend = Physics2D.OverlapCircle(transform.position, attackRadius, friendMask);

        if (friend && canAttack)
        {
            Kidnap(friend.gameObject);
        }
    }

    void Kidnap(GameObject friend)
    {
        Destroy(friend);
        animator.SetBool("WalkSide", false);
        spriteRenderer.flipX = false;
        StartCoroutine(Rest());
        eatFeedback.PlayFeedbacks();
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
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
