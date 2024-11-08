using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBucket : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private Button _button;
    [SerializeField] private Button _btnUpgrade;
    [SerializeField] PopcornBucket _popcornBucket;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = "$" + _price.ToString();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(BuyBucket);
    }

    public void BuyBucket()
    {
        if (_player.Money >= _price)
        {
            _popcornBucket.gameObject.SetActive(true);
            _btnUpgrade.interactable = true;
            gameObject.SetActive(false);
            _player.Money -= _price;
            _player.TextMoney.text = "$" + _player.Money.ToString();
        }
    }
}
