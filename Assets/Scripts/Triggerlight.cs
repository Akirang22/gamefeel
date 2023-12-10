using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Triggerlight : MonoBehaviour
{

    public Light2D freeformLight;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider is the player
        if (other.CompareTag("Player"))
        {
            // Implement your logic when the player enters the trigger area
            Debug.Log("Player entered the light trigger!");
        }
    }
}


