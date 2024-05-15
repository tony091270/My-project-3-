using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPiece : MonoBehaviour
{
    // Copy and paste your work, or start typing.
    public float lifetime = 2.0f;
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    private float flashSpeed = 0.25f;  // Speed of flashing
    [SerializeField] Sprite brokenSprite;

    void Start()
    {
        Debug.Log("Broken piece created");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        StartCoroutine(FlashBeforeDestroy());
    }

    IEnumerator FlashBeforeDestroy()
    {
        float elapsedTime = 0;
        bool isWhite = false;

        while (elapsedTime < lifetime)
        {
            elapsedTime += Time.deltaTime;
            if (isWhite)
            {
                spriteRenderer.sprite = brokenSprite;
                isWhite = false;
            }
            else
            {
                spriteRenderer.sprite = originalSprite;
                isWhite = true;
            }
            yield return new WaitForSeconds(flashSpeed);
            flashSpeed *= 0.9f;
        }

        Debug.Log("Destroying broken piece");
        Destroy(gameObject);
    }
}


