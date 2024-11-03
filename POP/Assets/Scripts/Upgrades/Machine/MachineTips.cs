using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineTips : MonoBehaviour
{
    private Player _player;
    [SerializeField] private float _increaseClientTips = 1.2f;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private TextMeshProUGUI _txPrice;
    [SerializeField] private List<PopcornBucket> _popcornBuckets;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(MoreTips);
    }

    public void MoreTips()
    {
        if (_player.Money >= _price)
        {
            _player.ClientTips *= _increaseClientTips;
            for (int i = 0; i < _popcornBuckets.Count - 1; i++)
            {
                _popcornBuckets[i].BucketPrice = (int)(_popcornBuckets[i].NumberOfPopcornsLimit / _player.BucketPriceDivider * _player.ClientTips);
                _popcornBuckets[i].TextMoney.UpdateText();
            }
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = _price.ToString() + " €";
        }
    }
}
