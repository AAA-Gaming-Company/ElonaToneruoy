using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;

    private Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x > 0)
        {
            animator.SetBool("isWalkingSide", true);
            spriteRenderer.flipX = false;
        }else
        {
            animator.SetBool("isWalkingSide", true);
            spriteRenderer.flipX = true;
        }
        if (x == 0)
        {
            animator.SetBool("isWalkingSide", false);
        }


        if (y > 0)
        {
            animator.SetBool("isWalkingUp", true);
        }
        else
        {
            animator.SetBool("isWalkingDown", true);
        }
        if (y == 0)
        {
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingUp", false);
        }

        Vector2 move = new Vector2(x * playerSpeed, y * playerSpeed);

        rigidbody.AddForce(move);
    }
}
