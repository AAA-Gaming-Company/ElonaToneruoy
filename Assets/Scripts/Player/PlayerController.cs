using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;

    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Camera cam;
    public MMF_Player hitFeedback;
    public LayerMask enemyLayers;
    public float range;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        UpdatePlayerSprite(x, y);
        rb.AddForce(new Vector2(x * playerSpeed, y * playerSpeed));

        if (Input.GetMouseButtonDown(0))
        {
            Hit();
            if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x )
            {
                spriteRenderer.flipX = false;
            }else
            {
                spriteRenderer.flipX = true;
            }
            hitFeedback.PlayFeedbacks();
        }
    }

    void Hit()
    {
        animator.SetTrigger("Hit");
        Collider2D collider = Physics2D.OverlapCircle(transform.position, range, enemyLayers);

        if (collider)
        {
            collider.GetComponent<EnemyBrain>().StartCoroutine(collider.GetComponent<EnemyBrain>().Stun());
        }
    }

    private void UpdatePlayerSprite(float x, float y)
    {
        if (x > 0)
        {
            animator.SetBool("isWalkingSide", true);
            spriteRenderer.flipX = false;

            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
            return; //Prioritise X over Y
        } else if (x < 0)
        {
            animator.SetBool("isWalkingSide", true);
            spriteRenderer.flipX = true;

            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
            return; //Prioritise X over Y
        } else
        {
            animator.SetBool("isWalkingSide", false);
        }

        if (y > 0)
        {
            animator.SetBool("isWalkingUp", true);
            animator.SetBool("isWalkingDown", false);
        } else if (y < 0)
        {
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isWalkingDown", true);
        } else
        {
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
