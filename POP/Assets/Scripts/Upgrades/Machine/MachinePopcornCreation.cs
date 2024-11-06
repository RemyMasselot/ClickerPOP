using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachinePopcornCreation : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " �";
        button = GetComponent<Button>();
        button.onClick.AddListener(PopcornCreation);
    }

    public void PopcornCreation()
    {
        if (_player.Money >= _price)
        {
            _player.PopNumber ++;
            _level++;
            _txLevel.text = "Nv " + _price.ToString();
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " �";
            _price = (int)(_price * _priceMultiplyer);
            _txPrice.text = _price.ToString() + " �";
        }
    }
}
