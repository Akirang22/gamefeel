using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Spritetest : MonoBehaviour

{
    public Sprite[] sprites;
    public Light2D freeformLight;
    private SpriteRenderer spriteRenderer;
    public CircleCollider2D circleCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Length > 0)
        {
            InvokeRepeating("SwitchSpriteRandomly", 0f, 2f); // Switch sprite every 2 seconds (adjust as needed)
        }
    }

    void SwitchSpriteRandomly()
    {
        if (sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];

            // Enable the light if a specific condition is met
            if (ShouldEmitLight(spriteRenderer.sprite))
            {
                freeformLight.enabled = true;

                enablecollider();
            }
            else
            {
                freeformLight.enabled = false;
               
            }


        }
    }

    bool ShouldEmitLight(Sprite currentSprite)
    {
        // Add your condition here, for example:
        return currentSprite.name == "walls-tileset-sliced_5";
        
    }


    void OnTriggerEnter2D(Collider2D GameObject)
    {
        // Check if the entering collider is the player
        if (GameObject.CompareTag("Light"))
        {
            // Implement logic for player death
            Die();
        }
    }

    void Die()
    {
        // Implement player death logic here
        // For example, you can reload the level or show a game over screen
        Debug.Log("Player died!");
    }

    void enablecollider()
    {
        circleCollider.enabled = true;

    }
}


