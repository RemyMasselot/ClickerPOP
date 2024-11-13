using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BurnPopcorn : MonoBehaviour
{
    private PopcornMachine _popcornMachine;
    private Player _player;
    private PopAnims _popAnims;
    [SerializeField] private SpriteRenderer _pop;
    [SerializeField] private Sprite _popDefault;
    public bool IsBurning;
    [SerializeField] private GameObject _btnShield;
    [SerializeField] private List<GameObject> _badPopcorns = new List<GameObject>();
    [SerializeField] private SpriteRenderer _flame;
    [SerializeField] private int _badPopcornLimit = 5;
    [SerializeField] private int _timeRebuild;
    [SerializeField] private CanvasGroup _canvaGroupCollider;

    private void Awake()
    {
        _popcornMachine = FindObjectOfType<PopcornMachine>();
        _player = FindObjectOfType<Player>();
        _popAnims = FindObjectOfType<PopAnims>();
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
        if (_player.ShieldActivated == true)
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
        _player.ShieldActivated = false;
        //Anim Save
        _popAnims.UpdateAnim("IsSaving", "HaveBoue", "IsBlowing");
        for (int i = 0; i < _player.PopcornBuckets.Count; i++)
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
            spriteRenderer.DOColor(Color.black, 1.5f).OnComplete(() =>
            {
                _flame.DOFade(1, 1f);
                //Anim Blow
                _popAnims.UpdateAnim("IsBlowing", "IsSaving", "HaveBoue");
                spriteRenderer.DOFade(0, 0.3f)
                .OnComplete(() =>
                {
                    Destroy(popcorn);
                });
            });
            return true;
        });
        _canvaGroupCollider.DOFade(0, 0.5f);
        StartCoroutine(BurnOff());
    }

    IEnumerator BurnOff()
    {
        yield return new WaitForSeconds(_timeRebuild);
        _badPopcorns.Clear();
        IsBurning = false;
        _btnShield.SetActive(true);
        _flame.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                //Anim Default
                _popAnims.UpdateAnim("IsSaving", "IsBlowing", "HaveBoue");
                
                _canvaGroupCollider.DOFade(1, 0.5f);
            });
    }
}
