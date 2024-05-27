using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour
{
    private enum UpgradeTypes
    {
        None,
        DoubleJump, 
        MetalShoes,
    }

    [SerializeField] private UpgradeTypes type;

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

                if (type == UpgradeTypes.DoubleJump)
                {

                    var playermovement = collision.gameObject.GetComponent<PlayerMovement>();
                    if (playermovement == null)
                    {
                        Debug.Log("No player movement");
                        return;
                    }
                    foreach (var contactpoint in collision.contacts)
                    {
                        if (contactpoint.normal.y > 0.5f)
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

                else if(type == UpgradeTypes.MetalShoes)
                 {

                var player = collision.gameObject.GetComponent<Player>();
                if (player == null)
                {
                    Debug.Log("No player movement");
                    return;
                }
                foreach (var contactpoint in collision.contacts)
                {
                    if (contactpoint.normal.y > 0.5f)
                    {
                        animator.SetTrigger("HitTrigger");
                        player.HasMetalShoes = true;
                        GetComponent<BoxCollider2D>().enabled = false;
                        itemCollector.HandleCherriesSpent(Cherriescost);
                        Destroy(gameObject, .7f);
                        break;
                    }
                }
            }


        }


    }
    private void OnDestroy()
    {
        if(gameObject.scene.isLoaded)
            Instantiate(BrokenPieces, transform.position, Quaternion.identity);
    }
}

