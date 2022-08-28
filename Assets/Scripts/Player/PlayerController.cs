using UnityEngine;
using MoreMountains.Feedbacks;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Camera cam;

    public MMF_Player hitFeedback;
    public LayerMask enemyLayers;
    public float range;

    public FriendManager friendManager;
    public GameStateManager gameStateManager;

    public bool hasFryingPan;

    public List<AudioSource> footstepSounds = new List<AudioSource>();
    private bool canPlaySound= true;

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

        if (x != 0 || y != 0)
        {
            StepSound();
            Debug.Log("sound");
        }

        UpdatePlayerSprite(x, y);
        rb.AddForce(new Vector2(x * playerSpeed, y * playerSpeed));

        if (Input.GetMouseButtonDown(0))
        {
            Hit();
            if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            } else
            {
                spriteRenderer.flipX = true;
            }

        }
    }

    public void Hit()
    {
        if (hasFryingPan)
        {
            hitFeedback.PlayFeedbacks();
            animator.SetTrigger("Hit");

            foreach (FriendBrain friend in friendManager.friends)
            {
                friend.Hit(range, enemyLayers);
            }

            Collider2D collider = Physics2D.OverlapCircle(transform.position, range, enemyLayers);

            if (collider)
            {
                EnemyBrain enemy = collider.GetComponent<EnemyBrain>();
                enemy.StartCoroutine(enemy.Stun());
            }
        }
      
    }

    void StepSound()
    {
        if (canPlaySound)
        {
            footstepSounds[Random.Range(0, footstepSounds.Count - 1)].Play();
            StartCoroutine(SoundCooldown());
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ColliderTrigger trigger = collider.gameObject.GetComponent<ColliderTrigger>();
        if (trigger != null)
        {
            if (trigger.GetIdentifier() == "ExitDoor")
            {
                gameStateManager.LevelComplete();
            }
        }
    }

    private IEnumerator SoundCooldown()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(.3f);
        canPlaySound = true;
    }
}
