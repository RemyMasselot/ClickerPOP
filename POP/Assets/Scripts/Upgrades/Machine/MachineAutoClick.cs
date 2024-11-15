using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditorInternal.ReorderableList;

public class MachineAutoClick : MonoBehaviour
{
    private Player _player;
    private Button button;
    private int _level = 0;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private float _divider = 2;
    [SerializeField] private TextMeshProUGUI _txTitle;
    [SerializeField] private TextMeshProUGUI _txDesc;
    [SerializeField] private TextMeshProUGUI _txPrice;
    [SerializeField] private TextMeshProUGUI _txLevel;
    [SerializeField] private string _txFirstUpgrade;
    [SerializeField] private string _txNextUpgrades;

    private Image _imageBtn;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _imageBtn = GetComponent<Image>();
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
            //Effect
            StartCoroutine(_player.StartAutoclickMachine());

            //Next buy
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.TextMoney.text = "$" + _player.Money.ToString();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(IncreaseAutoclick);
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
            _txDesc.text = _txNextUpgrades;

            //Visual
            _player.UpdateVisualCanBuy(_imageBtn);
        }
        else
        {
            _player.UpdateVisualCantBuy(_imageBtn);
        }
    }

    public void IncreaseAutoclick()
    {
        if (_player.Money >= _price)
        {
            //Effect
            _player.TimerAutoclick /= _divider;
            
            //Next Buy
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.TextMoney.text = "$" + _player.Money.ToString();
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
