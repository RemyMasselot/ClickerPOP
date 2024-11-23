using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewBucket : MonoBehaviour
{
    private Player _player;
    private Sentences _sentences;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private Button _button;
    [SerializeField] private Button _btnUpgrade;
    [SerializeField] PopcornBucket _popcornBucket;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _sentences = FindObjectOfType<Sentences>();
        _txPrice.text = "$" + _price.ToString();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(BuyBucket);
    }

    public void BuyBucket()
    {
        if (_player.Money >= _price)
        {
            _popcornBucket.gameObject.SetActive(true);
            _popcornBucket.BucketPrice = (int)(_popcornBucket.NumberOfPopcornsLimit / _player.BucketPriceDivider * _player.ClientTips);
            _popcornBucket.TextMoney.UpdateText();
            if (_sentences.IsFrench == true)
            {
                _sentences.SetFrench();
            }
            else
            {
                _sentences.SetEnglish();
            }
            _btnUpgrade.interactable = true;
            _btnUpgrade.GetComponent<Image>().color = _player.Default;
            _btnUpgrade.GetComponentInChildren<TextMeshProUGUI>().color = _player.Default;
            gameObject.SetActive(false);
            _player.Money -= _price;
            _player.TextMoney.text = "$" + _player.Money.ToString();
            _audioSource.clip = _player.SoundBuyUpgrade;
            _audioSource.Play();
        }
        else
        {
            _audioSource.clip = _player.SoundCantBuyUpgrade;
            _audioSource.Play();
        }
    }
}
