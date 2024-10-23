using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool removeRb = false;
    
    void Update()
    {
        // Passer l'ordre du calque devant celui de la casserole lorsque le popcorn retombe
        if (rb.velocity.y < 0 && spriteRenderer.sortingOrder == -4)
        {
            spriteRenderer.sortingOrder = -2;
            removeRb = true;
        }
        if (rb.velocity.y == 0)
        {
            rb.isKinematic = true;
        }
    }
}
