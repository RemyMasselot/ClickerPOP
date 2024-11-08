using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BtnUpgradesVisual : MonoBehaviour
{
    private Image _imageBtn;
    [SerializeField] private Color _default;
    [SerializeField] private Color _mouseEnter;
    [SerializeField] private Color _mousePress;

    private void Start()
    {
        _imageBtn = GetComponent<Image>();
    }

    void OnMouseEnter()
    {
        _mouseEnter.a = _imageBtn.color.a;
        _imageBtn.DOColor(_mouseEnter, 0.2f);
    }


    void OnMouseDown()
    {
        _mousePress.a = _imageBtn.color.a;
        _imageBtn.DOColor(_mousePress, 0.2f);
    }

    private void OnMouseUp()
    {
        _mouseEnter.a = _imageBtn.color.a;
        _imageBtn.DOColor(_mouseEnter, 0.2f);
    }

    void OnMouseExit()
    {
        _default.a = _imageBtn.color.a;
        _imageBtn.DOColor(_default, 0.2f);
    }
}
