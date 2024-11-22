using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class MachineAutoClick : MonoBehaviour
{
    private Player _player;
    private BurnPopcorn _burnPopcorn;
    private Button button;
    private Sentences _sentences;
    public int _level = 0;
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
    private AudioSource _audioSource;

    public int Part = 0;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _burnPopcorn = FindObjectOfType<BurnPopcorn>();
        _sentences = FindObjectOfType<Sentences>();
        _txLevel.text = "Lv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        _audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(UnlockAutoclick);
        //_txDesc.text = _sentences.MachineAutoclickTextsEN[Part];
    }

    public void UnlockAutoclick()
    {
        if (_player.Money >= _price)
        {
            if (Part == 0)
            {
                //Effect
                _player.WaitCoroutine = true;
                _player.CoroutinePreparation();
                StartCoroutine(_player.StartAutoclickMachine());

                //Next buy
                _level++;
                _txLevel.text = "Lv " + _level.ToString();
                _player.Money -= _price;
                _player.UpdateMoney(true);
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(IncreaseAutoclick);
                _price = (int)(_price * _priceMultiplyer);
                _player.UpdateText(_price, _txPrice);
                _txPrice.text = "$" + _txPrice.text;
                Part++;
                if (_sentences.IsFrench == true)
                {
                    _txDesc.text = _sentences.MachineAutoclickTextsFR[Part];
                }
                else
                {
                    _txDesc.text = _sentences.MachineAutoclickTextsEN[Part];
                }
                _txPrice.gameObject.transform.DOPunchScale(transform.localScale * -0.1f, 0.5f, 10, 0);

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

    public void IncreaseAutoclick()
    {
        if (_player.Money >= _price)
        {
            //Effect
            _player.TimerAutoclick /= _divider;

            //Next Buy
            _level++;
            _txLevel.text = "Lv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            _price = (int)(_price * _priceMultiplyer);
            _player.UpdateText(_price, _txPrice);
            _txPrice.text = "$" + _txPrice.text;
            _txPrice.gameObject.transform.DOPunchScale(transform.localScale * -0.1f, 0.5f, 10, 0);

            if (_level >= 8)
            {
                _burnPopcorn.DevilPopcornLimit = _level * 10;
            }

            //Visual
            _player.UpdateVisualCanBuy(gameObject.transform, _imageBtn);

            _audioSource.clip = _player.SoundBuyUpgrade;
            _audioSource.Play();
        }
        else
        {
            _player.UpdateVisualCantBuy(gameObject.transform, _imageBtn);
            _audioSource.clip = _player.SoundCantBuyUpgrade;
            _audioSource.Play();
        }
    }
}
