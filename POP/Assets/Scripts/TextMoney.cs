using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextMoney : MonoBehaviour
{
    private TextMeshProUGUI _textMoney;
    [SerializeField] private PopcornBucket _popcornBucket;
    [SerializeField] private float _alphaSpeed = 1;


    private void Start()
    {
        _textMoney = GetComponent<TextMeshProUGUI>();
        _textMoney.text = "+ " + _popcornBucket.BucketPrice.ToString() + "€";
    }

    public void Appeared()
    {
        _textMoney.alpha = 1;
        DOTween.To(() => _textMoney.alpha, x => _textMoney.alpha = x, 0, _alphaSpeed);
        transform.DOMoveY(transform.position.y + 0.5f, _alphaSpeed)
            .OnComplete(() =>
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            });
    }
}
