using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BtnShield : MonoBehaviour
{
    private Player _player;
    private PopAnims _popAnims;
    [SerializeField] private int _price = 10;
    [SerializeField] private TextMeshProUGUI _txPrice;
    [SerializeField] private GameObject _btnShield;
    [SerializeField] private ShowInfo _showInfo;
    //[SerializeField] private SpriteRenderer _pop;
    //[SerializeField] private Sprite _popBoue;
    private Button button;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _popAnims = FindObjectOfType<PopAnims>();
        _txPrice.text = "$" + _price.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(GetShield);
    }

    public void GetShield()
    {
        Debug.Log("ger");
        if (_player.Money >= _price)
        {
            //Anim Boue
            _popAnims.UpdateAnim("HaveBoue", "IsSaving", "IsBlowing");
            _showInfo.ShowMore();
            _btnShield.SetActive(false);
            _player.ShieldActivated = true;
            _player.Money -= _price;
            _player.TextMoney.text = "$" + _player.Money.ToString();
        }
    }
}