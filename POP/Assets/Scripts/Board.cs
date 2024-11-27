using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Board : MonoBehaviour
{
    private Button button;
    private bool _IsExpend = false;
    private BoxCollider2D _machineCollider;
    private Stats _stats;
    
    [SerializeField] private List<RectTransform> _rects = new List<RectTransform>();

    [SerializeField] private Transform _board;
    [SerializeField] private int _endHeight;
    [SerializeField] private int _boardEndPosY;
    [SerializeField] private Vector2 _machineColliderOffsetEnd;
    [SerializeField] private Vector2 _machineColliderSizeEnd;
    [SerializeField] private GameObject _fade;

    private float _boardStartPosY;
    private Vector2 _machineStartSize;
    private Vector2 _bucketsStartSize;
    private Vector2 _machineEndSize;
    private Vector2 _bucketsEndSize;
    private Vector2 _machineColliderOffsetStart;
    private Vector2 _machineColliderSizeStart;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _open;
    [SerializeField] private AudioClip _close;

    private void Awake()
    {
        PopcornMachine popcornMachine = FindObjectOfType<PopcornMachine>();
        _stats = FindObjectOfType<Stats>();
        _machineCollider = popcornMachine.GetComponent<BoxCollider2D>();
        _machineColliderOffsetStart = _machineCollider.offset;
        _machineColliderSizeStart = _machineCollider.size;

        _audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(BtnBoard);

        _boardStartPosY = _board.localPosition.y;
        
        _machineStartSize = new Vector2(_rects[0].rect.width, _rects[0].rect.height);
        _machineEndSize = new Vector2(_rects[0].rect.width, _endHeight);
        
        _bucketsStartSize = new Vector2(_rects[1].rect.width, _rects[1].rect.height);
        _bucketsEndSize = new Vector2(_rects[1].rect.width, _endHeight);
    }

    public void BtnBoard()
    {
        if (_IsExpend == false)
        {
            BoardExpenssion();
        }
        else
        {
            BoardContraction();
        }
    }
    public void BoardExpenssion()
    {
        _IsExpend = true;
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * 0.2f, 0.3f, 10, 0);
        transform.DOLocalMoveY(transform.localPosition.y - 8, 0.2f)
            .OnComplete(() =>
            {
                for (int i = 0; i < _rects.Count; i++)
                {
                    if (i == 0)
                    {
                        _rects[i].DOSizeDelta(_machineEndSize, 0.5f);
                    }
                    else
                    {
                        _rects[i].DOSizeDelta(_bucketsEndSize, 0.5f);
                    }
                }
                _board.DOLocalMoveY(_boardEndPosY, 0.5f);
                DOTween.To(() => _machineCollider.offset, x => _machineCollider.offset = x, _machineColliderOffsetEnd, 0.4f);
                DOTween.To(() => _machineCollider.size, x => _machineCollider.size = x, _machineColliderSizeEnd, 0.4f);
                transform.DOLocalMoveY(transform.localPosition.y + 8, 0.05f);
                _fade.GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
                if (_stats.OnBtnStats == true)
                {
                    _stats.ShowStatsContent();
                }
                _fade.SetActive(false);
                _audioSource.clip = _open;
                _audioSource.Play();
            });
    }

    public void BoardContraction()
    {
        _IsExpend = false;
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * 0.2f, 0.3f, 10, 0);
        transform.DOLocalMoveY(transform.localPosition.y - 10, 0.2f)
            .OnComplete(() =>
            {
                for (int i = 0; i < _rects.Count; i++)
                {
            
                    if (i == 0)
                    {
                        int y = i;
                        _rects[y].DOSizeDelta(_machineStartSize, 0.5f)
                            .OnComplete(() =>
                            {
                                Scrollbar scroll = _rects[y].GetComponentInChildren<Scrollbar>();
                                DOTween.To(() => scroll.value, x => scroll.value = x, 1, 0.35f);
                            });
                    }
                    else
                    {
                        int y = i;
                        _rects[y].DOSizeDelta(_bucketsStartSize, 0.5f)
                                .OnComplete(() =>
                                {
                                    Scrollbar scroll = _rects[y].GetComponentInChildren<Scrollbar>();
                                    DOTween.To(() => scroll.value, x => scroll.value = x, 1, 0.35f);
                                });
                    }
                }
                _board.DOLocalMoveY(_boardStartPosY, 0.5f);
                DOTween.To(() => _machineCollider.offset, x => _machineCollider.offset = x, _machineColliderOffsetStart, 0.5f);
                DOTween.To(() => _machineCollider.size, x => _machineCollider.size = x, _machineColliderSizeStart, 0.5f);
                transform.DOLocalMoveY(transform.localPosition.y + 10, 0.05f);
                _fade.SetActive(true);
                _fade.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
                if (_stats.OnBtnStats == true)
                {
                    _stats.OnBtnStats = false;
                    _stats.HideStatsContent();
                }
                _audioSource.clip = _close;
                _audioSource.Play();
            });
    }
}
