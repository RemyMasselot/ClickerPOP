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
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    [SerializeField] private Transform _burnLimit;
    [SerializeField] private List<Transform> _burnTargets = new List<Transform>();
    private Button button;
    [SerializeField] private Image _imageBtn;

    [SerializeField] private BtnShield _btnShield;
    [SerializeField] private TextMeshProUGUI _priceTextShield;
    [SerializeField] private int _priceShieldMultiplyer = 5;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txLevel.text = "Nv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        _audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(MoreTips);
    }

    public void MoreTips()
    {
        if (_player.Money >= _price)
        {
            if (_indexTarget < _burnTargets.Count)
            {
                _burnLimit.transform.DOMoveY(_burnTargets[_indexTarget].transform.position.y, _timeMove);
                _btnShield.Price *= _priceShieldMultiplyer;
                _priceTextShield.text = _btnShield.Price.ToString();
                _indexTarget++;
                _level++;
                _txLevel.text = "Nv " + _level.ToString();
                _player.Money -= _price;
                _player.UpdateMoney(true);
                if (_indexTarget == _burnTargets.Count)
                {
                    _txLevel.text = "Nv " + _level.ToString();
                    _txPrice.text = "MAX";
                    button.onClick.RemoveListener(MoreTips);
                    button.interactable = false;
                }
                else
                {
                    _price = (int)(_price * _priceMultiplyer);
                    _player.UpdateText(_price, _txPrice);
                    _txPrice.text = "$" + _txPrice.text;
                }

                //Visual
                _player.UpdateVisualCanBuy(gameObject.transform, _imageBtn);

                _audioSource.clip = _player.SoundBuyUpgrade;
                _audioSource.Play();
            }
        }
        else
        {
            _player.UpdateVisualCantBuy(gameObject.transform, _imageBtn);
            _audioSource.clip = _player.SoundCantBuyUpgrade;
            _audioSource.Play();
        }
    }
}
