using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxbg : MonoBehaviour
{
   

    public float parallaxSpeed = 0.5f; // Adjust this value to control the parallax speed
    private Transform playerTransform;
    private Rigidbody2D playerRigidbody;
    private Vector3 lastPlayerPosition;

    private void Start()
    {
        // Assuming the player is tagged as "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
            lastPlayerPosition = playerTransform.position;
        }
        else
        {
            Debug.LogError("No object with tag 'Player' found.");
        }
    }

    private void Update()
    {
        if (playerTransform != null && playerRigidbody != null && playerTransform.position != lastPlayerPosition)
        {
            float playerVelocityX = playerRigidbody.velocity.x;
            float cameraX = Camera.main.transform.position.x;

            // Iterate through each child (background layer) and move it
            foreach (Transform layer in transform)
            {
                float parallaxFactor = 1 - layer.position.z * parallaxSpeed;
                float layerTargetX = cameraX - playerVelocityX * parallaxFactor;

                // Smoothly move the layer towards the target position
                Vector3 layerPosition = new Vector3(layerTargetX, layer.position.y, layer.position.z);
                layer.position = Vector3.Lerp(layer.position, layerPosition, Time.deltaTime * parallaxSpeed);
            }

            lastPlayerPosition = playerTransform.position;
        }
    }
}
