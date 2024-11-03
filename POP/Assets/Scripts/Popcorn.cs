using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    public Rigidbody2D Rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int BounceCount = 0;
    public bool CanBurn = false;
    
    void Update()
    {
        if (Rb.IsSleeping() == false)
        {
            if (spriteRenderer.sortingOrder == -3)
            {
                if (Mathf.Abs(Rb.velocity.y) <= 0.0005)
                {
                    Rb.Sleep();
                    CanBurn = true;
                }
            }
            else if (Rb.velocity.y < 0)
            {
                spriteRenderer.sortingOrder = -3;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceCount++;
    }
}
