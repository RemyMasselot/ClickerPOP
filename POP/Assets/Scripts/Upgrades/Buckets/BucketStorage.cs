using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketStorage : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private float _storageMultiplyer = 2;
    [SerializeField] private float _priceMultiplyer = 1.5f;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(IncreaseStorage);
    }

    public void IncreaseStorage()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket _bucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _bucket.NumberOfPopcornsLimit = (int)(_bucket.NumberOfPopcornsLimit * _storageMultiplyer);
            _bucket.SliderUpdate();
            _bucket.BucketPrice = (int)(_bucket.NumberOfPopcornsLimit / _player.BucketPriceDivider * _player.ClientTips);
            _bucket.TextMoney.UpdateText();
            _level++;
            _txLevel.text = "Nv " + _price.ToString();
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = _price.ToString() + " €";
        }
    }
}
