using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;

public class BurnPopcorn : MonoBehaviour
{
    private PopcornMachine _popcornMachine;
    private Player _player;
    private PopAnims _popAnims;
    [SerializeField] private SpriteRenderer _pop;
    [SerializeField] private Sprite _popDefault;
    public bool IsBurning;
    [SerializeField] private GameObject _btnShield;
    [SerializeField] private CanvasGroup _toolTip;
    public List<GameObject> BadPopcorns = new List<GameObject>();
    public List<GameObject> DevilPopcorns = new List<GameObject>();
    [SerializeField] private SpriteRenderer _flame;
    [SerializeField] private int _badPopcornLimit = 5;
    public int DevilPopcornLimit = 20;
    [SerializeField] private int _timeRebuild;
    [SerializeField] private CanvasGroup _canvaGroupCollider;
    private AudioSource _audioSource;

    private void Awake()
    {
        _popcornMachine = FindObjectOfType<PopcornMachine>();
        _player = FindObjectOfType<Player>();
        _popAnims = FindObjectOfType<PopAnims>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Popcorn>() == true)
        {
            if (collision.gameObject.GetComponent<Popcorn>().CanBurn == true)
            {
                if (BadPopcorns.Contains(collision.gameObject) == false)
                {
                    BadPopcorns.Add(collision.gameObject);
                    if (BadPopcorns.Count >= _badPopcornLimit)
                    {
                        if (IsBurning == false)
                        {
                            IsBurning = true;
                            _btnShield.SetActive(false);
                            _toolTip.DOFade(0, 0.5f);
                            CheckShield();
                            return;
                        }
                    }
                }
            }
            if (DevilPopcorns.Contains(collision.gameObject) == false)
            {
                DevilPopcorns.Add(collision.gameObject);
                if (DevilPopcorns.Count >= DevilPopcornLimit)
                {
                    if (IsBurning == false)
                    {
                        IsBurning = true;
                        _btnShield.SetActive(false);
                        _toolTip.DOFade(0, 0.5f);
                        CheckShield();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Popcorn>() == true)
        {
            if (collision.gameObject.GetComponent<Popcorn>().CanBurn == true)
            {
                if (BadPopcorns.Contains(collision.gameObject) == true)
                {
                    if (IsBurning == false)
                    {
                        BadPopcorns.Remove(collision.gameObject);
                    }
                }
            }
            if (DevilPopcorns.Contains(collision.gameObject) == true)
            {
                if (IsBurning == false)
                {
                    DevilPopcorns.Remove(collision.gameObject);
                }
            }
        }
    }

    private void CheckShield()
    {
        if (_player.ShieldActivated == true)
        {
            SaveAnim();
            BurnAllPopcorn();
        }
        else
        {
            BurnAllPopcorn();
        }
    }

    private void SaveAnim()
    {
        //Anim Save
        _popAnims.UpdateAnim("IsSaving", "HaveBoue", "IsBlowing");
    }

    private void SaveSomePopcorns()
    {
        _player.ShieldActivated = false;
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
        _audioSource.Play();
        if (_player.ShieldActivated == true)
        {
            SaveSomePopcorns();
        }
        _popcornMachine.PopcornList.RemoveAll(popcorn =>
        {
            
            SpriteRenderer spriteRenderer = popcorn.GetComponent<SpriteRenderer>();
            spriteRenderer.DOColor(Color.black, 0.5f)
                .OnComplete(() =>
                {
                    _flame.DOFade(1, 0.5f);
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
        BadPopcorns.Clear();
        DevilPopcorns.Clear();
        IsBurning = false;
        DOTween.To(() => _audioSource.volume, x => _audioSource.volume = x, 0, 0.5f)
            .OnComplete(() =>
            {
                _audioSource.Stop();
                _audioSource.volume = 1;
            });
        
        _flame.DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                //Anim Default
                _popAnims.UpdateAnim("IsSaving", "IsBlowing", "HaveBoue");
                DOVirtual.DelayedCall(1, () =>
                {
                    _canvaGroupCollider.DOFade(1, 0.5f);
                    _btnShield.SetActive(true);
                });
            });
    }
}
