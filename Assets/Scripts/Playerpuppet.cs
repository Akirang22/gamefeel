using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerpuppet : MonoBehaviour
{
    //For particle system
    public ParticleSystem Dust; // Assign your "Dust" particle system in the Unity Editor
    public string groundLayer = "Ground"; // Set the ground layer name

    //public AudioSource Walksound;

    public string targetTag = "Heart";
    public string sTag = "Killer";

    //private bool isGrounded = false;
    //private bool hasJumped = false;
   // private float jumpTimer = 0f;
  // private float jumpDelay = 1f;

    //player declaration
    private Rigidbody2D player;
    public float speed = 5f;
    public float jumpforce = 10f;
    public Vector3 Transform;

    //animation 
    public Animator animator;
    public SpriteRenderer spriterenderer;

    //for gravity 
    private bool isGravityReversed = false;
    private bool Spaceispressed = false;
    
    //for boxcast
    public Vector3 boxSize;
    public float maxDistance;  
    public LayerMask layerMask;

    //public AudioSource AudioWalk;

    //respawn
    public Transform spawnPoint; // Set this in the Unity Editor to the desired spawn point
    public float respawnTime = 3f;
    private bool playerIsDead = false;
    public GameObject mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned!");
        }
    }
    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        //WALK
        float moveHorizontal = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(moveHorizontal * speed, player.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        //AudioWalk.Play();
        
        
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

        if (Input.GetKey(KeyCode.A) && isGravityReversed)
        {
            spriterenderer.flipX = false;

        }
        if (Input.GetKey(KeyCode.D) && isGravityReversed)
        {
            spriterenderer.flipX = true;
        }

        
        //JUMP
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            player.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            player.freezeRotation = true;
            animator.SetBool("Spaceispressed", true);
        }
        if (Input.GetKeyUp(KeyCode.Space)) // Check if space key is released
        {
            animator.SetBool("Spaceispressed", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && SkyCheck())
        {
            player.AddForce(-transform.up * jumpforce, ForceMode2D.Impulse);
            player.freezeRotation = true;
        }
        // Check if the player collides with an object on the specified layer

       


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Heart"))
        {
            Destroy(col.gameObject);
            
        }
        if (col.gameObject.CompareTag("Killer")) // Adjust the tag based on your enemy GameObject
        {
            if (!playerIsDead)
            {
                playerIsDead = true;
                StartCoroutine(RespawnPlayer());
            }
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Dust.Play();
        }
    }

    IEnumerator RespawnPlayer()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, mainCamera.transform.position.z);
        }

        // Respawn the player at the spawn point
        transform.position = spawnPoint.position;

        // Additional respawn logic (reset health, etc.) can be added here

        playerIsDead = false;
        yield break;
    }    

    //BOXCAST
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

    bool SkyCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.up, maxDistance, layerMask))
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
            //spriterenderer.flipY = true;
            transform.rotation = Quaternion.Euler(Vector3.forward*180);
            Physics2D.gravity = new Vector2(0, 9.8f); // Reverse gravity                                                   
            //transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else
        {

            //spriterenderer.flipY = false;
            transform.rotation = Quaternion.Euler(Vector3.forward);
            Physics2D.gravity = new Vector2(0, -9.8f); // Restore normal gravity
            //transform.eulerAngles = Vector3.zero; // Reset rotation
        }
    }

   
    
}
