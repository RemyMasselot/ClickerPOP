using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class PopcornBucket : MonoBehaviour
{
    private Button _button;
    private Player _player;
    private BurnPopcorn _burnPopcorn;
    private Stats _stats;
    private Vector3 _startPos;
    public PopcornMachine PopcornMachine;

    [SerializeField] private List<Texture> Images = new List<Texture>();

    public int NumberOfPopcornsCurrent = 0;
    public int NumberOfPopcornsLimit = 10;

    [SerializeField] private Hands handLeft;
    [SerializeField] private Hands handRight;

    private Slider _slider;
    private CanvasGroup canvasGroup;
    [SerializeField] private float _alphaSpeed = 1;

    [SerializeField] private RawImage _rawImageBucket;
    [SerializeField] private RawImage _rawImageShadow;
    public Image Timer;
    public float TimerDuration = 2f;

    public int BucketPrice = 2;
    public float TimerAutoclick = 1;
    public TextMoneyGained TextMoney;

    public int FillNumber = 1;
    [SerializeField] private CanvasGroup _toolTipBucket;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(CickOnBucket);
        _player = FindObjectOfType<Player>();
        _burnPopcorn = FindObjectOfType<BurnPopcorn>();
        _stats = FindObjectOfType<Stats>();

        _startPos = transform.localPosition;
        _slider = GetComponentInChildren<Slider>();
        SliderUpdate();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        BucketPrice = (int)(NumberOfPopcornsLimit / _player.BucketPriceDivider * _player.ClientTips);
    }

    public void CickOnBucket()
    {
        if (_burnPopcorn.IsBurning == false)
        {
            if (PopcornMachine.PopcornList.Count > 0)
            {
                RepeatFillTheBucket(FillNumber);
            }
            else
            {
                _toolTipBucket.gameObject.SetActive(true);
                _toolTipBucket.DOKill();
                _toolTipBucket.DOFade(1, 0.5f)
                    .OnComplete(() =>
                    {
                        _toolTipBucket.DOKill();
                        DOVirtual.DelayedCall(2, () =>
                        {
                            _toolTipBucket.DOFade(0, 0.5f)
                            .OnComplete(() =>
                            {
                                _toolTipBucket.gameObject.SetActive(false);
                            });
                        });
                    });
            }
        }
    }

    public void RepeatFillTheBucket(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (NumberOfPopcornsCurrent < NumberOfPopcornsLimit)
            {
                // Prendre un popcorn
                FillTheBucket();
            }
            else
            {
                break;
            }
        }
    }

    public void FillTheBucket()
    {
        if (_player.UsePhysic == true)
        {
            FillWithPhysic();
        }
        else
        {
            FillWithoutPhysic();
        }
    }

    private void FillWithPhysic()
    {
        for (int i = 0; i <= PopcornMachine.PopcornList.Count - 1; i++)
        {
            Popcorn popcorn = PopcornMachine.PopcornList[i].GetComponent<Popcorn>();
            if (popcorn.BounceCount > 1)
            {
                //Supprimer le popcorn de la liste burncollider
                if (_burnPopcorn.BadPopcorns.Contains(popcorn.gameObject))
                {
                    _burnPopcorn.BadPopcorns.Remove(popcorn.gameObject);
                }
                //Anim bucket
                Transform visualShadow = _rawImageShadow.GetComponent<Transform>();
                Transform visualBucket = _rawImageBucket.GetComponent<Transform>();
                visualShadow.DOKill(true);
                visualShadow.DOPunchScale(visualShadow.localScale * 0.6f, 0.5f, 10, 0.8f);
                visualBucket.DOKill(true);
                visualBucket.DOPunchScale(visualBucket.localScale * 0.2f, 0.5f, 10, 0.5f);
                // Supprimer le popcorn de la liste des popcorn contenus dans la Popcorn Machine
                GameObject popcornRemoved = PopcornMachine.PopcornList[i];
                PopcornMachine.PopcornList.RemoveAt(i);
                // Détruire ce popcorn
                Destroy(popcornRemoved);
                NumberOfPopcornsCurrent++;
                _slider.value = NumberOfPopcornsCurrent;

                // Mettre a jour le sprite du bucket selon le nombre de popcorns contenus
                if (NumberOfPopcornsCurrent == NumberOfPopcornsLimit)
                {
                    _rawImageBucket.texture = Images[3];
                    ChangeTheBucket();
                    _player.BucketsSold++;
                    _player.CheckBucketLimits();
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 1.3f)
                {
                    HandsReady();
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 1.5f)
                {
                    _rawImageBucket.texture = Images[2];
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 3)
                {
                    _rawImageBucket.texture = Images[1];
                    return;
                }
                break;
            }
        }
    }

    private void FillWithoutPhysic()
    {
        for (int i = PopcornMachine.PopcornList.Count - 1; i >= 0; i--)
        {
            Popcorn popcorn = PopcornMachine.PopcornList[i].GetComponent<Popcorn>();
            if (popcorn.BounceCount > 1)
            {
                //Supprimer le popcorn de la liste burncollider
                if (_burnPopcorn.BadPopcorns.Contains(popcorn.gameObject))
                {
                    _burnPopcorn.BadPopcorns.Remove(popcorn.gameObject);
                }
                //Anim bucket
                Transform visualShadow = _rawImageShadow.GetComponent<Transform>();
                Transform visualBucket = _rawImageBucket.GetComponent<Transform>();
                visualShadow.DOKill(true);
                visualShadow.DOPunchScale(visualShadow.localScale * 0.6f, 0.5f, 10, 0.8f);
                visualBucket.DOKill(true);
                visualBucket.DOPunchScale(visualBucket.localScale * 0.2f, 0.5f, 10, 0.5f);
                // Supprimer le popcorn de la liste des popcorn contenus dans la Popcorn Machine
                GameObject popcornRemoved = PopcornMachine.PopcornList[i];
                PopcornMachine.PopcornList.RemoveAt(i);
                // Détruire ce popcorn
                Destroy(popcornRemoved);
                NumberOfPopcornsCurrent++;
                _slider.value = NumberOfPopcornsCurrent;

                // Mettre a jour le sprite du bucket selon le nombre de popcorns contenus
                if (NumberOfPopcornsCurrent == NumberOfPopcornsLimit)
                {
                    _rawImageBucket.texture = Images[3];
                    ChangeTheBucket();
                    _player.BucketsSold++;
                    _player.CheckBucketLimits();
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 1.3f)
                {
                    HandsReady();
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 1.5f)
                {
                    _rawImageBucket.texture = Images[2];
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 3)
                {
                    _rawImageBucket.texture = Images[1];
                    return;
                }
                break;
            }
        }
    }

    public void SliderUpdate()
    {
        _slider.maxValue = NumberOfPopcornsLimit;
    }

    public void BucketLeave()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, _alphaSpeed)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(0.1f, () =>
                {
                    Vector2 endMove = new Vector2(transform.position.x, transform.position.y + 0.3f);
                    transform.DOMove(endMove, 0.2f)
                    .OnComplete(() =>
                    {
                        Vector2 endMove = new Vector2(transform.position.x, transform.position.y - 2.5f);
                        transform.DOMove(endMove, 0.2f)
                        .OnComplete(() =>
                         {
                             ReplaceBucket();
                             Reload();
                         });
                    });
                    _rawImageShadow.DOFade(0, 0.2f);
                    _player.Money += BucketPrice;
                    _player.UpdateMoney(false);
                    TextMoney.gameObject.SetActive(true);
                    TextMoney.Appeared();
                    _stats.TotalMoney += BucketPrice;
                    _stats.TotalBucketSold++;
                });
            });
    }

    private void HandsReady()
    {
        handLeft.GoToTarget2();
        handRight.GoToTarget2();
    }

    private void ChangeTheBucket()
    {
        handLeft.GoToTarget3();
        handRight.GoToTarget3();
        BucketLeave();
    }

    private void ReplaceBucket()
    {
        Color colorBucket = _rawImageBucket.color;
        colorBucket.a = 0;
        _rawImageBucket.color = colorBucket;
        transform.localPosition = _startPos;
        handLeft.GoToTarget1();
        handRight.GoToTarget1();
    }
    
    private void Reload()
    {
        Timer.DOFade(1, 0.3f)
            .OnComplete(() =>
            {
                DOTween.To(() => Timer.fillAmount, x => Timer.fillAmount = x, 1, TimerDuration)
                .OnComplete(() =>
                {
                    Timer.DOFade(0, 0.3f);
                    Timer.fillAmount = 0;
                    NumberOfPopcornsCurrent = 0;
                    _rawImageBucket.DOFade(1, 0.3f);
                    _rawImageShadow.DOFade(1, 0.3f);
                    _slider.value = NumberOfPopcornsCurrent;
                    DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, _alphaSpeed);
                    _rawImageBucket.texture = Images[0];
                });
            });
    }

    public IEnumerator StartAutoclickBucket()
    {
        while (_burnPopcorn.IsBurning == true)
        {
            yield return null;
        }
        if (PopcornMachine.PopcornList.Count > 0)
        {
            if (NumberOfPopcornsCurrent < NumberOfPopcornsLimit)
            {
                FillTheBucket();
            }
        }
        yield return new WaitForSeconds(TimerAutoclick);
        StartCoroutine(StartAutoclickBucket());
    }
}
