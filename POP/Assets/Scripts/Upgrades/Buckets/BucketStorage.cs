using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketStorage : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private float _storageIncrease = 2;
    [SerializeField] private float _priceMultiplyer = 1.5f;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    private Button button;
    [SerializeField] private Image _imageBtn;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txLevel.text = "Nv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(IncreaseStorage);
    }

    public void IncreaseStorage()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket _bucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _bucket.NumberOfPopcornsLimit = (int)(_bucket.NumberOfPopcornsLimit + _storageIncrease);
            _bucket.SliderUpdate();
            _bucket.BucketPrice = (int)(_bucket.NumberOfPopcornsLimit / _player.BucketPriceDivider * _player.ClientTips);
            _bucket.TextMoney.UpdateText();
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
            
            //Visual
            _player.UpdateVisualCanBuy(gameObject.transform, _imageBtn);
        }
        else
        {
            _player.UpdateVisualCantBuy(gameObject.transform, _imageBtn);
        }
    }
}
