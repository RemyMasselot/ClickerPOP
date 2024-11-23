using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketFill : MonoBehaviour
{
    private Player _player;
    private CanvasGroup _canvasGroup;
    private BucketCond _bucketCond;
    [SerializeField] private int _index = 0;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    private Button button;
    [SerializeField] private Image _imageBtn;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txLevel.text = "Lv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        _audioSource = GetComponent<AudioSource>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _bucketCond = GetComponentInChildren<BucketCond>();
        button = GetComponent<Button>();
        button.onClick.AddListener(FillBucket);
    }

    private void Update()
    {
        if (_bucketCond.gameObject.activeSelf == false)
        {
            if (_player.Money >= _price)
            {
                _canvasGroup.alpha = 1;
            }
            else
            {
                _canvasGroup.alpha = 0.7f;
            }
        }
    }

    public void FillBucket()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket _bucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _bucket.FillNumber ++;
            _level++;
            _txLevel.text = "Lv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            _price = (int)(_price * _priceMultiplyer);
            _player.UpdateText(_price, _txPrice);
            _txPrice.text = "$" + _txPrice.text;

            //Visual
            _player.UpdateVisualCanBuy(gameObject.transform, _imageBtn);

            _audioSource.clip = _player.SoundBuyUpgrade;
            _audioSource.Play();
        }
        else
        {
            _player.UpdateVisualCantBuy(gameObject.transform, _imageBtn);
            _audioSource.clip = _player.SoundCantBuyUpgrade;
            _audioSource.Play();
        }
    }
}
