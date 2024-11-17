using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    private Player _player;
    public Rigidbody2D Rb;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRendererMachine;
    public int BounceCount = 0;
    public bool CanBurn = false;
    [SerializeField] private Color _newColor;


    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceCount++;
        if (BounceCount == 2)
        {
            _spriteRenderer.color = _newColor;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_player.UsePhysic == true)
        {
            if (Rb.IsSleeping() == false)
            {
                if (_spriteRenderer.sortingOrder == _spriteRendererMachine.sortingOrder - 1)
                {
                    if (Mathf.Abs(Rb.velocity.y) <= 0.0005)
                    {
                        Rb.Sleep();
                        CanBurn = true;
                    }
                }
                else if (Rb.velocity.y < 0)
                {
                    _spriteRenderer.sortingOrder = _spriteRendererMachine.sortingOrder - 1;
                    //Debug.Log(spriteRenderer.sortingOrder);
                }
            }
        }
        else
        {
            if (Rb.bodyType != RigidbodyType2D.Kinematic)
            {
                if (_spriteRenderer.sortingOrder == _spriteRendererMachine.sortingOrder - 1)
                {
                    if (Mathf.Abs(Rb.velocity.y) <= 0.0005)
                    {
                        Rb.bodyType = RigidbodyType2D.Kinematic;
                        Rb.velocity = new Vector3 (0,0,0);
                        Rb.angularVelocity = 0;
                        CanBurn = true;
                    }
                }
                else if (Rb.velocity.y < 0)
                {
                    _spriteRenderer.sortingOrder = _spriteRendererMachine.sortingOrder - 1;
                }
            }
        }
    }
}
