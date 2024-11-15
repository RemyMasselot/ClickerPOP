using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    public Rigidbody2D Rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRendererMachine;
    public int BounceCount = 0;
    public bool CanBurn = false;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Rb.IsSleeping() == false)
        {
            if (spriteRenderer.sortingOrder == spriteRendererMachine.sortingOrder - 1)
            {
                if (Mathf.Abs(Rb.velocity.y) <= 0.0005)
                {
                    Rb.Sleep();
                    CanBurn = true;
                }
            }
            else if (Rb.velocity.y < 0)
            {
                spriteRenderer.sortingOrder = spriteRendererMachine.sortingOrder - 1;
                //Debug.Log(spriteRenderer.sortingOrder);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceCount++;
    }
}
