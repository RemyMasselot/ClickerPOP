using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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
            //Effect
            StartCoroutine(_player.StartAutoclickMachine());

            //Next buy
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(IncreaseAutoclick);
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
            _txDesc.text = _txNextUpgrades;
            _txPrice.gameObject.transform.DOPunchScale(transform.localScale * -0.1f, 0.5f, 10, 0);

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
            //Effect
            _player.TimerAutoclick /= _divider;
            
            //Next Buy
            _level++;
            _txLevel.text = "Nv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = "$" + _price.ToString();
            _txPrice.gameObject.transform.DOPunchScale(transform.localScale * -0.1f, 0.5f, 10, 0);

            //Visual
            _player.UpdateVisualCanBuy(gameObject.transform, _imageBtn);
        }
        else
        {
            _player.UpdateVisualCantBuy(gameObject.transform, _imageBtn);
        }
    }
}
