using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BucketAutoclick : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private float _divider = 2;
    [SerializeField] private TextMeshProUGUI _txTitle;
    [SerializeField] private TextMeshProUGUI _txDesc;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    [SerializeField] private string _txFirstUpgrade;
    [SerializeField] private string _txNextUpgrades;
    private Coroutine _coroutine;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(UnlockAutoclick);
        _txDesc.text = _txFirstUpgrade;
    }

    public void UnlockAutoclick()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket popcornBucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _coroutine = StartCoroutine(_player.StartAutoclickBucket(_index, popcornBucket.TimerAutoclick));
            _level++;
            _txLevel.text = "Nv " + _price.ToString();
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(IncreaseAutoclick);
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = _price.ToString() + " €";
            _txDesc.text = _txNextUpgrades;
            //Debug.Log("oui");
        }
    }

    public void IncreaseAutoclick()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket popcornBucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            popcornBucket.TimerAutoclick /= _divider;
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(_player.StartAutoclickBucket(_index, popcornBucket.TimerAutoclick));
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = _price.ToString() + " €";
            //Debug.Log("haha");
        }
    }
}
