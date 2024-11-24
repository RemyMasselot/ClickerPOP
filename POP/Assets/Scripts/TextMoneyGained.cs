using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextMoneyGained : MonoBehaviour
{
    private TextMeshProUGUI _textMoney;
    [SerializeField] private PopcornBucket _popcornBucket;
    [SerializeField] private float _alphaSpeed = 1;


    private void Start()
    {
        _textMoney = GetComponent<TextMeshProUGUI>();
        _textMoney.text = "$" + _popcornBucket.BucketPrice.ToString();
    }

    public void UpdateText()
    {
        _textMoney.text = "$" + _popcornBucket.BucketPrice.ToString();
    }

    public void Appeared()
    {
        transform.DOMoveY(transform.position.y + 0.7f, _alphaSpeed);
        _textMoney.alpha = 1;
        DOTween.To(() => _textMoney.alpha, x => _textMoney.alpha = x, 0, _alphaSpeed).SetDelay(1)
            .OnComplete(() =>
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.7f, transform.position.z);
                gameObject.SetActive(false);
            });
    }
}
