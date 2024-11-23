using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BucketCond : MonoBehaviour
{
    public int NumBucketsToSell;
    private Button _button;
    private Player _player;
    private TextMeshProUGUI _text;
    private Image _image;
    [SerializeField] private GameObject _childContent;
    [SerializeField] private GameObject _content;
    [SerializeField] private Image _btnRenderer;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _image = GetComponent<Image>();
        _button = GetComponentInParent<Button>();
        _button.interactable = false;
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = _player.BucketsSold + "/" + NumBucketsToSell.ToString();
    }


    public void CheckNumBuckets()
    {
        _text.text = _player.BucketsSold + "/" + NumBucketsToSell.ToString();
        if (_player.BucketsSold >= NumBucketsToSell)
        {
            _button.interactable = true;
            if (_content != null)
            {
                _content.SetActive(true);
            }
            if (_childContent != null)
            {
                _childContent.SetActive(false);
            }
            _player.UpdateVisualCanBuy(gameObject.transform, _image);
            _image.DOFade(0, 0.5f);
            _btnRenderer.DOFade(1, 0.5f)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
        }
    }
}
