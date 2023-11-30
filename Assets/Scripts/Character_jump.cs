using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_jump : MonoBehaviour

{
    Rigidbody2D rb;
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public float jumpforce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck() )
        {
            rb.AddForce(transform.up*jumpforce,ForceMode2D.Impulse);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawCube(transform.position-transform.up*maxDistance,boxSize);
    }

    private bool GroundCheck()
    {
        if(Physics2D.BoxCast(transform.position,boxSize,0,-transform.up,maxDistance,layerMask))
        {
            return true;
        }
        else{
            return false;
        }
    }
    
}
