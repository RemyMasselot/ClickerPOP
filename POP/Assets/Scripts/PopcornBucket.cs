using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopcornBucket : MonoBehaviour
{
    private Vector3 _startPos;
    
    public PopcornMachine PopcornMachine;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> Sprites = new List<Sprite>();

    public int NumberOfPopcornsCurrent = 0;
    public int NumberOfPopcornsLimit = 10;

    [SerializeField] private Hands handLeft;
    [SerializeField] private Hands handRight;

    private Slider _slider;
    private CanvasGroup canvasGroup;
    [SerializeField] private float _alphaSpeed = 1;

    [SerializeField] private SpriteRenderer _spriteRendererBucket;
    [SerializeField] private SpriteRenderer _spriteRendererShadow;
    public Image Timer;
    public float TimerDuration = 2f;

    private Player _player;
    public int BucketPrice = 5;
    public float TimerAutoclick = 1;
    public TextMoneyGained TextMoney;

    public int LvAutoclick;
    public int LvFill;
    public int LvStorage;
    public int FillNumber = 1;

    private void Awake()
    {
        _startPos = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _slider = GetComponentInChildren<Slider>();
        SliderUpdate();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        _player = FindAnyObjectByType<Player>();
        BucketPrice = (int)(NumberOfPopcornsLimit / _player.BucketPriceDivider * _player.ClientTips);
    }

    public void RepeatFillTheBucket()
    {
        for (int i = 0; i < FillNumber; i++)
        {
            FillTheBucket();
        }
    }

    public void FillTheBucket()
    {
        for (int i = PopcornMachine.PopcornList.Count-1; i >= 0; i--)
        {
            Popcorn popcorn = PopcornMachine.PopcornList[i].GetComponent<Popcorn>();
            if (popcorn.BounceCount > 1)
            {
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
                    spriteRenderer.sprite = Sprites[3];
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
                    spriteRenderer.sprite = Sprites[2];
                    return;
                }
                else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 3)
                {
                    spriteRenderer.sprite = Sprites[1];
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
                    _spriteRendererShadow.DOFade(0, 0.2f);
                    _player.GainMoney(BucketPrice);
                    TextMoney.Appeared();
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
        Color colorBucket = _spriteRendererBucket.color;
        colorBucket.a = 0;
        _spriteRendererBucket.color = colorBucket;
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
                    _spriteRendererBucket.DOFade(1, 0.3f);
                    _spriteRendererShadow.DOFade(1, 0.3f);
                    _slider.value = NumberOfPopcornsCurrent;
                    DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1, _alphaSpeed);
                    spriteRenderer.sprite = Sprites[0];
                });
            });
    }
}
