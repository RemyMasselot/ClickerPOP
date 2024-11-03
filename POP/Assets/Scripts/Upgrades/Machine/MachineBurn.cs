using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MachineBurn : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _indexTarget = 1;
    [SerializeField] private float _timeMove = 1;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private TextMeshProUGUI _txPrice;
    [SerializeField] private Transform _burnLimit;
    [SerializeField] private List<Transform> _burnTargets = new List<Transform>();
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txPrice.text = _price.ToString() + " €";
        button = GetComponent<Button>();
        button.onClick.AddListener(MoreTips);
    }

    public void MoreTips()
    {
        if (_player.Money >= _price)
        {
            _burnLimit.transform.DOMoveY(_burnTargets[_indexTarget].transform.position.y, _timeMove);
            _indexTarget++;
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString() + " €";
            if (_indexTarget == _burnTargets.Count)
            {
                _txPrice.text = "MAX";
                button.onClick.RemoveListener(MoreTips);
                button.interactable = false;
            }
            else
            {
                _price = (int)(_price * _priceMultiplyer);
                _txPrice.text = _price.ToString() + " €";
            }
        }
    }
}
