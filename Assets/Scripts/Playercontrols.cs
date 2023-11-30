using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Playercontrols : MonoBehaviour

{
    //declarations for playermovement

    private Rigidbody2D player;
    public float speed = 0.200f;
    public float jumpforce = 10f;

    //declarations for anims
    public SpriteRenderer spriterenderer;
    public Animator animator;

    //for collectibles
    public string targetTag = "Strawberry";
    public string sTag = "Spikes";

    //for boxcast
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;

    //for gravity reveresed
    private bool isGravityReversed = false;

    public string vinesTag = "Vines";
    public float slowdownFactor = 0.5f;


    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //player velocity while doing actions (mostly running on the floor)
        float v = Input.GetAxis("Horizontal");
        float moveHorizontal = v;
        player.velocity = new Vector2(moveHorizontal * speed, player.velocity.y);


        //player jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck() && !isGravityReversed)
        {
            player.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            player.freezeRotation = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGravityReversed)
        {
            player.AddForce(-transform.up * jumpforce, ForceMode2D.Impulse);
            player.freezeRotation = true;
        }


        //anim

        animator.SetFloat("speed", Mathf.Abs(moveHorizontal));

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

    //For collectible and spikes
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag(sTag))
        {
            Destroy(gameObject);
            LoadNextScene();
        }


    }
    //for youdied scene
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(nextSceneIndex);
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
    //gravity reversed
    void UpdateGravity()
    {
        if (isGravityReversed)
        {
            Physics2D.gravity = new Vector2(0, 9.8f); // Reverse gravity
                                                      //transform.Rotate(0f, 180f, 0f);

        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.8f); // Restore normal gravity
        }
    }
}

    