using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BucketAutoclick : MonoBehaviour
{
    private Player _player;
    [SerializeField] private PopcornBucket _popcornBucket;
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
    private Button button;
    [SerializeField] private Image _imageBtn;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txLevel.text = "Nv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(UnlockAutoclick);
        _txDesc.text = _txFirstUpgrade;
    }

    public void UnlockAutoclick()
    {
        if (_player.Money >= _price)
        {
            StartCoroutine(_popcornBucket.StartAutoclickBucket());
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(IncreaseAutoclick);
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
            _txDesc.text = _txNextUpgrades;
            
            //Visual
            _player.UpdateVisualCanBuy(gameObject.transform, _imageBtn);
        }
        else
        {
            _player.UpdateVisualCantBuy(gameObject.transform, _imageBtn);
        }
    }

    public void IncreaseAutoclick()
    {
        if (_player.Money >= _price)
        {
            _popcornBucket.TimerAutoclick /= _divider;
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
