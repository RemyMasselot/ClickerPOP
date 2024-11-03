using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BurnPopcorn : MonoBehaviour
{
    private PopcornMachine _popcornMachine;
    private Player _player;
    public bool IsBurning;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _btnShield;
    [SerializeField] private List<GameObject> _badPopcorns = new List<GameObject>();
    [SerializeField] private int _badPopcornLimit = 5;

    private void Awake()
    {
        _popcornMachine = FindObjectOfType<PopcornMachine>();
        _player = FindObjectOfType<Player>();
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
                            IsBurning = true;
                            _btnShield.SetActive(false);
                            CheckShield();
                        }
                    }
                }
            }
        }
    }

    private void CheckShield()
    {
        if (_shield.activeSelf == true)
        {
            SaveSomePopcorns();
            BurnAllPopcorn();
        }
        else
        {
            BurnAllPopcorn();
        }
    }

    private void SaveSomePopcorns()
    {
        _shield.SetActive(false);
        for (int i = 0; i < _player.PopcornBuckets.Count-1; i++)
        {
            if (_player.PopcornBuckets[i].activeSelf == true)
            {
                PopcornBucket _popcornBucket = _player.PopcornBuckets[i].GetComponent<PopcornBucket>();
                int num = _popcornBucket.NumberOfPopcornsLimit - _popcornBucket.NumberOfPopcornsCurrent;
                _popcornBucket.RepeatFillTheBucket(num);
            }
        }
    }

    private void BurnAllPopcorn()
    {
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
        _btnShield.SetActive(true);
        //Debug.Log(IsBurning);
    }
}
