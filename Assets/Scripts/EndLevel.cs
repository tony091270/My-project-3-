using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement;
        playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (collision.gameObject.CompareTag("Finish"))
        {
            
        }
    }
}
