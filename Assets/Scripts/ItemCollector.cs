using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{

    private int cherries = 0;
    public int Cherries => cherries;

    [SerializeField] private Text cherriesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
            Debug.Log("Cherries: " + cherries);
        }
    }


    public void HandleCherriesSpent(int CherriesSpent)
    {
        cherries -= CherriesSpent;
    }
}
