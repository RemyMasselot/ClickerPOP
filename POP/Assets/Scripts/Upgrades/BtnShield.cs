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
    public int Price = 10;
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
        _txPrice.text = "$" + Price.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(GetShield);
    }

    public void GetShield()
    {
        //Debug.Log("ger");
        if (_player.Money >= Price)
        {
            //Anim Boue
            _popAnims.UpdateAnim("HaveBoue", "IsSaving", "IsBlowing");
            _showInfo.ShowMore();
            _btnShield.SetActive(false);
            _player.ShieldActivated = true;
            _player.Money -= Price;
            _player.UpdateMoney(true);
        }
    }
}