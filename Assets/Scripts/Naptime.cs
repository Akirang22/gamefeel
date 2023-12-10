using UnityEngine;

public class Naptime : MonoBehaviour

{
    private Animator animatorplay;
    private string PlayerTag = "Player";
    private Vector3 lockedPlayerPosition;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag (PlayerTag))
        {
            playNapwithFrens();
        }
    }

    void playNapwithFrens()
    {
        animatorplay.SetTrigger("YourAnimationTriggerName");
        transform.position = lockedPlayerPosition;
        Debug.Log("slay bestie");
        
    }

}
