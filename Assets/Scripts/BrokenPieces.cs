using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    [SerializeField] float explosionForce = 1f;


    // Start is called before the first frame update
    void Start()
    {
        Explode();
        Destroy(gameObject, 2f);
    }

    private void Explode()
    {
        foreach(Transform piece in transform)
        {
            var rigidbody = piece.GetComponent<Rigidbody2D>();
            if(rigidbody != null)
            {
                Vector2 explosiondirection = piece.position - transform.position;
                explosiondirection.Normalize();
                rigidbody.AddForce(explosiondirection * explosionForce, ForceMode2D.Impulse);
            }
        }
    }

}
