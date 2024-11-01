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
    [SerializeField] private Image _timer;
    public float TimerDuration = 2f;

    private Player _player;
    public int BucketPrice = 5;
    [SerializeField] private TextMoneyGained _textMoney;

    private void Start()
    {
        _startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        _slider = GetComponentInChildren<Slider>();
        SliderUpdate();
        canvasGroup = GetComponentInChildren<CanvasGroup>();

        _player = FindAnyObjectByType<Player>();
    }

    public void FillTheBucket()
    {
        if (PopcornMachine.PopcornList.Count > 2)
        {
            // Supprimer le popcorn de la liste des popcorn contenus dans la Popcorn Machine
            GameObject popcornRemoved = PopcornMachine.PopcornList[PopcornMachine.PopcornList.Count - 2];
            PopcornMachine.PopcornList.RemoveAt(PopcornMachine.PopcornList.Count - 2);
            // Détruire ce popcorn
            Destroy(popcornRemoved);
        }
        else
        {
            // Supprimer le popcorn de la liste des popcorn contenus dans la Popcorn Machine
            GameObject popcornRemoved = PopcornMachine.PopcornList[PopcornMachine.PopcornList.Count - 1];
            PopcornMachine.PopcornList.RemoveAt(PopcornMachine.PopcornList.Count - 1);
            // Détruire ce popcorn
            Destroy(popcornRemoved);
        }
        

        NumberOfPopcornsCurrent ++;
        _slider.value = NumberOfPopcornsCurrent;

        // Mettre a jour le sprite du bucket selon le nombre de popcorns contenus
        if (NumberOfPopcornsCurrent == NumberOfPopcornsLimit)
        {
            spriteRenderer.sprite = Sprites[3];
            ChangeTheBucket();
            return;
        }
        else if (NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 1.3f)
        {
            HandsReady();
            return;
        }
        else if(NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 1.5f)
        {
            spriteRenderer.sprite = Sprites[2];
            return;
        }
        else if(NumberOfPopcornsCurrent >= NumberOfPopcornsLimit / 3)
        {
            spriteRenderer.sprite = Sprites[1];
            return;
        }
    }

    private void SliderUpdate()
    {
        _slider.maxValue = NumberOfPopcornsLimit;
    }

    public void BucketLeave()
    {
        DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, _alphaSpeed)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    Vector2 endMove = new Vector2(transform.position.x, transform.position.y + 0.3f);
                    transform.DOMove(endMove, 0.3f)
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
                    _textMoney.Appeared();
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
        transform.position = _startPos;
        handLeft.GoToTarget1();
        handRight.GoToTarget1();
    }
    
    private void Reload()
    {
        _timer.DOFade(1, 0.3f)
            .OnComplete(() =>
            {
                DOTween.To(() => _timer.fillAmount, x => _timer.fillAmount = x, 1, TimerDuration)
                .OnComplete(() =>
                {
                    _timer.DOFade(0, 0.3f);
                    _timer.fillAmount = 0;
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
