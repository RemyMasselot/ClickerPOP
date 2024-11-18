using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private bool _isShowing = false;
    private Image _image;

    public int TotalMoney;
    public int TotalPopcorn;
    //public int TotalPopcornBurned;
    public int TotalBucketSold;

    public TextMeshProUGUI MeshTotalMoney;
    public TextMeshProUGUI MeshTotalPopcorn;
    //public TextMeshProUGUI MeshTotalPopcornBurned;
    public TextMeshProUGUI MeshTotalBucketSold;

    [SerializeField] private GameObject _btnMachine;
    [SerializeField] private GameObject _btnBucket;
    [SerializeField] private GameObject _Upgrades;
    [SerializeField] private GameObject _fade;
    [SerializeField] private GameObject _statsContent;

    [SerializeField] private Sprite _goOn;
    [SerializeField] private Sprite _goOut;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ShowStats()
    {
        if (_isShowing == false)
        {
            _isShowing = true;
            MeshTotalMoney.text = TotalMoney.ToString();
            MeshTotalPopcorn.text = TotalPopcorn.ToString();
            //MeshTotalPopcornBurned.text = TotalPopcornBurned.ToString();
            MeshTotalBucketSold.text = TotalBucketSold.ToString();
            _statsContent.SetActive(true);
            _btnMachine.SetActive(false);
            _btnBucket.SetActive(false);
            _Upgrades.SetActive(false);
            _fade.SetActive(false);
            _image.sprite = _goOut;
        }
        else
        {
            _isShowing = false;
            _statsContent.SetActive(false);
            _btnMachine.SetActive(true);
            _btnBucket.SetActive(true);
            _Upgrades.SetActive(true);
            _fade.SetActive(true);
            _image.sprite = _goOn;
        }
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);
    }
}
