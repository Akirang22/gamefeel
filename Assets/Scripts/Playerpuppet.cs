using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerpuppet : MonoBehaviour
{
    private Rigidbody2D player;
    public float speed = 5f;
    public float jumpforce = 10f;

    public Animator animator;
    public SpriteRenderer spriterenderer;

    private bool isGravityReversed = false;
    private bool Spaceispressed = false;

    //for boxcast
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //player velocity while doing actions (mostly running on the floor)
        float moveHorizontal = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(moveHorizontal * speed, player.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        //player jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            player.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            player.freezeRotation = true;
            animator.SetBool("Spaceispressed",true);
        }
        if (Input.GetKeyUp(KeyCode.Space)) // Check if space key is released
        {
            animator.SetBool("Spaceispressed", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGravityReversed)
        {
            player.AddForce(-transform.up * jumpforce, ForceMode2D.Impulse);
            player.freezeRotation = true;
        }

        //flip player when turn
        if (Input.GetKey(KeyCode.A))
        {
            spriterenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            spriterenderer.flipX = false;
        }

        //Reverse gravity
        if (Input.GetKeyDown(KeyCode.K))
        {
            isGravityReversed = !isGravityReversed;
            UpdateGravity();
        }

        
    }

    //for raycast
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
    bool GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void UpdateGravity()
    {
        if (isGravityReversed)
        {
            Physics2D.gravity = new Vector2(0, 9.8f); // Reverse gravity
                                                      //transform.Rotate(0f, 180f, 0f);
            spriterenderer.flipY = true;
        }

        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.8f); // Restore normal gravity
            spriterenderer.flipY = false;
        }
    }

    
}
