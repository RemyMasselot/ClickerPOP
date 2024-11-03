using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnShield : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    [SerializeField] private GameObject _shield;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(GetShield);
    }

    public void GetShield()
    {
        if (_player.Money >= _price)
        {
            _shield.SetActive(true);
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            gameObject.SetActive(false);
        }
    }
}