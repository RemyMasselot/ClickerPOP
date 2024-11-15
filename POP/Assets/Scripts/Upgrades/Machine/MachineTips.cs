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
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    [SerializeField] private List<PopcornBucket> _popcornBuckets;
    private Button button;
    private Image _imageBtn;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _imageBtn = GetComponent<Image>();
        _txLevel.text = "Nv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
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
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney();
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
            
            //Visual
            _player.UpdateVisualCanBuy(_imageBtn);
        }
        else
        {
            _player.UpdateVisualCantBuy(_imageBtn);
        }
    }
}
