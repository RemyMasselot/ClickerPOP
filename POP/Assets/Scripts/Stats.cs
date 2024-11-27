using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private Player _player;
    private bool _isShowing = false;
    public bool OnBtnStats = false;
    private Image _image;
    private Board _board;

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
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _board = FindObjectOfType<Board>();
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isShowing == true)
        {
            _player.UpdateText(TotalMoney, MeshTotalMoney);
            _player.UpdateText(TotalPopcorn, MeshTotalPopcorn);
            //MeshTotalPopcornBurned.text = TotalPopcornBurned.ToString();
            _player.UpdateText(TotalBucketSold, MeshTotalBucketSold);
        }
    }

    public void ShowStats()
    {
        if (_player.WaitCoroutine == false)
        {
            OnBtnStats = true;
            if (_isShowing == false)
            {
                _board.BoardExpenssion();
            }
            else
            {
                _board.BoardContraction();
            }
            transform.DOKill(true);
            transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);

            _audioSource.Play();
        }
    }

    public void ShowStatsContent()
    {
        _isShowing = true;
        //_player.UpdateText(TotalMoney, MeshTotalMoney);
        //_player.UpdateText(TotalPopcorn, MeshTotalPopcorn);
        ////MeshTotalPopcornBurned.text = TotalPopcornBurned.ToString();
        //_player.UpdateText(TotalBucketSold, MeshTotalBucketSold);
        _statsContent.SetActive(true);
        _statsContent.GetComponent<CanvasGroup>().DOFade(1, 0.6f);
        _btnMachine.GetComponentInParent<CanvasGroup>().DOFade(0, 0.2f);
        _fade.GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
        _Upgrades.GetComponent<CanvasGroup>().DOFade(0, 0.2f)
            .OnComplete(() =>
            {
                _Upgrades.SetActive(false);
                _btnMachine.SetActive(false);
                _btnBucket.SetActive(false);
                _fade.SetActive(false);
            });
        _image.sprite = _goOut;
    }

    public void HideStatsContent()
    {
        _isShowing = false;
        _statsContent.GetComponent<CanvasGroup>().DOFade(0, 0.2f)
            .OnComplete(() =>
            {
                _statsContent.SetActive(false);
            });
        _Upgrades.SetActive(true);
        _btnMachine.SetActive(true);
        _btnBucket.SetActive(true);
        _fade.SetActive(true);
        _btnMachine.GetComponentInParent<CanvasGroup>().DOFade(1, 0.6f);
        _fade.GetComponent<SpriteRenderer>().DOFade(1, 0.6f);
        _Upgrades.GetComponent<CanvasGroup>().DOFade(1, 0.6f);
        _image.sprite = _goOn;
    }
}
