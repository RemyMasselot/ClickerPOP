using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineAutoClick : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _price = 10;
    [SerializeField] private int _priceMultiplyer = 3;
    [SerializeField] private TextMeshProUGUI _txTitle;
    [SerializeField] private TextMeshProUGUI _txDesc;
    [SerializeField] private TextMeshProUGUI _txPrice;
    [SerializeField] private string _txFirstUpgrade;
    [SerializeField] private string _txNextUpgrades;
    private Button button;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " �";
        button = GetComponent<Button>();
        button.onClick.AddListener(UnlockAutoclick);
        _txDesc.text = _txFirstUpgrade;
    }

    public void UnlockAutoclick()
    {
        if (_player.Money >= _price)
        {
            StartCoroutine(_player.StartAutoclickMachine());
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " �";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(IncreaseAutoclick);
            _price *= _priceMultiplyer;
            _txPrice.text = _price.ToString() + " �";
            _txDesc.text = _txNextUpgrades;
            //Debug.Log("oui");
        }
    }

    public void IncreaseAutoclick()
    {
        if (_player.Money >= _price)
        {
            _player.TimerAutoclick *= 0.5f;
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " �";
            _price *= _priceMultiplyer;
            _txPrice.text = _price.ToString() + " �";
            //Debug.Log("haha");
        }
    }
}
