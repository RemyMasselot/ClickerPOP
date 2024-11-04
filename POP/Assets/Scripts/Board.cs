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
    
    [SerializeField] private List<RectTransform> _rects = new List<RectTransform>();

    [SerializeField] private Transform _board;
    [SerializeField] private int _endHeight;
    [SerializeField] private int _boardEndPosY;
    [SerializeField] private Vector2 _machineColliderOffsetEnd;
    [SerializeField] private Vector2 _machineColliderSizeEnd;

    private float _boardStartPosY;
    private Vector2 _machineStartSize;
    private Vector2 _bucketsStartSize;
    private Vector2 _machineEndSize;
    private Vector2 _bucketsEndSize;
    private Vector2 _machineColliderOffsetStart;
    private Vector2 _machineColliderSizeStart;


    private void Awake()
    {
        PopcornMachine popcornMachine = FindObjectOfType<PopcornMachine>();
        _machineCollider = popcornMachine.GetComponent<BoxCollider2D>();
        _machineColliderOffsetStart = _machineCollider.offset;
        _machineColliderSizeStart = _machineCollider.size;

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
            _IsExpend = true;
            BoardExpenssion();
        }
        else
        {
            _IsExpend = false;
            BoardContraction();
        }
    }
    public void BoardExpenssion()
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
    }

    public void BoardContraction()
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
    }
}
