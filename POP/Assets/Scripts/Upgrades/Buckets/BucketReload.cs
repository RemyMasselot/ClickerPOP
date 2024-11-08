using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketReload : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private int _timeDivider = 2;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txLevel.text = "Nv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(DecreaseReloadTime);
    }

    public void DecreaseReloadTime()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket _bucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _bucket.TimerDuration /= _timeDivider;
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.TextMoney.text = "$" + _player.Money.ToString();
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
        }
    }
}
