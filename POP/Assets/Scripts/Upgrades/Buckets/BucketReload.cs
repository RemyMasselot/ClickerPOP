using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BucketReload : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private float _timeDivider = 2;
    [SerializeField] private float _priceMultiplyer = 3;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    private int _level = 0;
    [SerializeField] private TextMeshProUGUI _txLevel;
    private Button button;
    [SerializeField] private Image _imageBtn;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _txLevel.text = "Lv " + _level.ToString();
        _txPrice.text = "$" + _price.ToString();
        _audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(DecreaseReloadTime);
    }

    public void DecreaseReloadTime()
    {
        if (_player.Money >= _price)
        {
            PopcornBucket _bucket = _player.PopcornBuckets[_index].GetComponent<PopcornBucket>();
            _bucket.TimerDuration /= _timeDivider;
            _level++;
            _txLevel.text = "Lv " + _level.ToString();
            _player.Money -= _price;
            _player.UpdateMoney(true);
            _price = (int)(_price * _priceMultiplyer);
            _player.UpdateText(_price, _txPrice);
            _txPrice.text = "$" + _txPrice.text;

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
