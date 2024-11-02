using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BucketFill : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
    }

    public void FillBucket()
    {
        if (_player.Money >= _price)
        {
            _player.FillNumber ++;
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            _price *= 3;
            _txPrice.text = _price.ToString() + " €";
        }
    }
}
