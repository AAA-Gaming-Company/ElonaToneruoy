using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;

public class FriendBrain : MonoBehaviour
{
    private Transform player;
    private FriendManager manager;
    private AIPath path;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        path = GetComponent<AIPath>();
        InvokeRepeating("SetDestination", 0.3f, 0.3f);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateWalk());
    }

    private void SetDestination()
    {
        path.destination = player.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
    }

    private IEnumerator AnimateWalk()
    {
        Vector3 oldPos = transform.position;

        yield return new WaitForSeconds(.5f);

        float positionDiff = transform.position.x - oldPos.x;

        if (positionDiff > 0)
        {
            animator.SetBool("isWalkingSide", true);
            spriteRenderer.flipX = false;
        }
        else if (positionDiff < 0)
        {
            animator.SetBool("IsWalkingSide", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("WalkSide", false);
        }

        StartCoroutine(AnimateWalk());

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

    public void Hit(float range, LayerMask enemyLayers)
    {
        animator.SetTrigger("Hit");
        Collider2D collider = Physics2D.OverlapCircle(transform.position, range, enemyLayers);

        if (collider)
        {
            EnemyBrain enemy = collider.GetComponent<EnemyBrain>();
            enemy.StartCoroutine(enemy.Stun());
        }
    }

}
