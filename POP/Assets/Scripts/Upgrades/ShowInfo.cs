using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowInfo : MonoBehaviour
{
    private Button button;
    [SerializeField] private CanvasGroup _toolTip;
    private bool _selected = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowMore);
    }

    public void ShowMore()
    {
        if (_selected == false)
        {
            _selected = true;
            _toolTip.DOFade(1, 0.5f);
        }
        else
        {
            _selected = false;
            _toolTip.DOFade(0, 0.5f);
        }
    }
}
