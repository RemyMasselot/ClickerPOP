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
    private Button button;
    [SerializeField] PopcornBucket _popcornBucket;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(BuyBucket);
    }

    public void BuyBucket()
    {
        if (_player.Money >= _price)
        {
            _popcornBucket.gameObject.SetActive(true);
            gameObject.SetActive(false);
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
        }
    }
}
