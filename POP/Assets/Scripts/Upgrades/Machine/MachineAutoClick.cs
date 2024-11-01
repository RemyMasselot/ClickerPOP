using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MachineAutoClick : MonoBehaviour
{
    [SerializeField] private PopcornMachine _popcornMachine;
    [SerializeField] private Player _player;
    [SerializeField] private int _price = 10;
    [SerializeField] private float _timerAutoclick = 1;
    [SerializeField] private TextMeshProUGUI _txTitle;
    [SerializeField] private TextMeshProUGUI _txDesc;
    [SerializeField] private TextMeshProUGUI _txPrice;

    private void Start()
    {
        _txPrice.text = _price.ToString();
    }


    public void UnlockAutoclick()
    {
        if (_player.Money >= _price)
        {
            StartCoroutine(StartAutoclick());
            _player.Money -= _price;
            _player.TextMoney.text = _player.Money.ToString();
            Button button = GetComponent<Button>();
            button.onClick.RemoveListener(UnlockAutoclick);
            button.onClick.AddListener(IncreaseAutoclick);
            _price *= 3;
            _txPrice.text = _price.ToString();
        }
    }

    IEnumerator StartAutoclick()
    {
        _popcornMachine.PopAPopcorn();
        yield return new WaitForSeconds(_timerAutoclick);
        StartCoroutine(StartAutoclick());
    }

    public void IncreaseAutoclick()
    {
        if (_player.Money >= _price)
        {
            _timerAutoclick *= 1.2f;
        }
    }
}
