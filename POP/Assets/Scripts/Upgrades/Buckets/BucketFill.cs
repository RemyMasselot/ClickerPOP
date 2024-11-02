using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketFill : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private int _price = 10;
    [SerializeField] private int _priceMultiplyer = 3;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(FillBucket);
    }

    public void FillBucket()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket _bucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _bucket.FillNumber ++;
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            _price *= _priceMultiplyer;
            _txPrice.text = _price.ToString() + " €";
        }
    }
}
