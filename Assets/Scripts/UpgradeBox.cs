using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour
{
    [SerializeField] int Cherriescost;
    public GameObject BrokenPieces;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var itemCollector = collision.gameObject.GetComponent<ItemCollector>();
            if(itemCollector == null)
            {
                Debug.Log("No Cherries Found");
                return;
            }
            if(itemCollector.Cherries < Cherriescost)
            {
                Debug.Log("Not enough Cherries");
                return;
            }
            var playermovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playermovement == null)
            {
                Debug.Log("No player movement");
                return;
            }
            foreach(var contactpoint in collision.contacts)
            {
                if(contactpoint.normal.y>0.5f)
                {
                    animator.SetTrigger("HitTrigger");
                    playermovement.candoublejump = true;
                    GetComponent<BoxCollider2D>().enabled = false;
                    itemCollector.HandleCherriesSpent(Cherriescost);
                    Destroy(gameObject, .7f);
                    break;
                }
            }
        }
    }
    private void OnDestroy()
    {
        Instantiate(BrokenPieces, transform.position, Quaternion.identity);
    }
}

