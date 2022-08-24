using System.Collections;
using UnityEngine;
using Pathfinding;
using MoreMountains.Feedbacks;

public class EnemyBrain : MonoBehaviour
{
    public LayerMask friendMask;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float attackRadius = 1;

    public MMF_Player damageFeedback;
    public MMF_Player eatFeedback;

    private AIDestinationSetter aIDestinationSetter;
    private AIPath path;

    private WaitForSeconds walkCooldown;
    private WaitForSeconds restCooldown;
    private bool isResting;
    private WaitForSeconds stunCooldown1;
    private WaitForSeconds stunCooldown2;
    private bool isStunnedInternal;
    private bool isStunned;

    public void Start()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        
        InvokeRepeating("ScanForTargets", 1f, 1f);

        StartCoroutine(Attack());
        StartCoroutine(AnimateWalk());
        path = GetComponent<AIPath>();

        walkCooldown = new WaitForSeconds(0.5f);
        restCooldown = new WaitForSeconds(12f);
        stunCooldown1 = new WaitForSeconds(5f);
        stunCooldown2 = new WaitForSeconds(1f);
        isStunned = false;
        isStunnedInternal = false;
    }

    public void ScanForTargets()
    {
        Collider2D[] friends = Physics2D.OverlapCircleAll(transform.position, 200, friendMask);

        if (friends.Length != 0)
        {
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

    private IEnumerator AnimateWalk()
    {
        Vector3 oldPos = transform.position;

        yield return walkCooldown;

        float positionDiff = transform.position.x - oldPos.x;

        if (positionDiff > 0)
        {
            animator.SetBool("WalkSide", true);
            spriteRenderer.flipX = false;
        } else if (positionDiff < 0)
        {
            animator.SetBool("WalkSide", true);
            spriteRenderer.flipX = true;
        } else
        {
            animator.SetBool("WalkSide", false);
        }

        StartCoroutine(AnimateWalk());

    }

    private IEnumerator Attack()
    {
        Collider2D friendCollider = Physics2D.OverlapCircle(transform.position, attackRadius, friendMask);

        if (friendCollider != null && friendCollider.gameObject != null && !isStunned && !isResting) //Make sure that the enemy isn't stunned (or already resting??? shouldn't happen)
        {
            isResting = true;
            path.canMove = false;

            animator.SetBool("WalkSide", false);
            spriteRenderer.flipX = false;

            FriendBrain friendBrain = friendCollider.gameObject.GetComponent<FriendBrain>();
            friendBrain.GetEaten();

            eatFeedback.PlayFeedbacks();

            yield return restCooldown;

            if (!isStunned) //Before allowing moving, check if the enemy is stunned
            {
                path.canMove = true;
            }
            isResting = false;
        }

        yield return null;
        StartCoroutine(Attack());
    }

    public IEnumerator Stun()
    {
        if (!isStunned || !isStunnedInternal)
        {
            damageFeedback.PlayFeedbacks();
            isStunnedInternal = true;
            isStunned = true;
            path.canMove = false;
            animator.SetBool("isStunned", true);

            yield return stunCooldown1;

            //Give the player a bit of time to stun again
            isStunnedInternal = false;
            yield return stunCooldown2;

            if (!isStunnedInternal)
            {
                if (!isResting) //Before allowing moving, check if the enemy is resting
                {
                    path.canMove = true;
                }

                isStunned = false;
                animator.SetBool("isStunned", false);

            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
