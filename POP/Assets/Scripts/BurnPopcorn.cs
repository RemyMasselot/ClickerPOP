using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BurnPopcorn : MonoBehaviour
{
    private PopcornMachine _popcornMachine;
    public bool IsBurning;
    [SerializeField] private List<GameObject> _badPopcorns = new List<GameObject>();
    [SerializeField] private int _badPopcornLimit = 5;

    private void Awake()
    {
        _popcornMachine = FindObjectOfType<PopcornMachine>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Popcorn>() == true)
        {
            if (collision.gameObject.GetComponent<Popcorn>().CanBurn == true)
            {
                if (_badPopcorns.Contains(collision.gameObject) == false)
                {
                    _badPopcorns.Add(collision.gameObject);
                    if (_badPopcorns.Count >= _badPopcornLimit)
                    {
                        if (IsBurning == false)
                        {
                            BurnAllPopcorn();
                            //Debug.Log("ge");
                        }
                    }
                }
            }
        }
    }

    private void BurnAllPopcorn()
    {
        IsBurning = true;
        _popcornMachine.PopcornList.RemoveAll(popcorn =>
        {
            SpriteRenderer spriteRenderer = popcorn.GetComponent<SpriteRenderer>();
            spriteRenderer.DOColor(Color.black, 1).OnComplete(() =>
            {
                Destroy(popcorn);
                //Debug.Log("destroy");
            });
            return true;
        });
        Invoke("BurnOff", 5);
        //Debug.Log(IsBurning);
    }

    private void BurnOff()
    {
        _badPopcorns.Clear();
        IsBurning = false;
        //Debug.Log(IsBurning);
    }
}
