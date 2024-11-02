using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int BounceCount = 0;
    
    void Update()
    {
        if (spriteRenderer.sortingOrder == -3)
        {
            if (rb.velocity.y == 0 && rb.IsSleeping() == false)
            {
                rb.Sleep();
            }
        }
        else if (rb.velocity.y < 0)
        {
            spriteRenderer.sortingOrder = -3;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceCount++;
    }
}
