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
    private AudioSource _audioSource;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowMore);
        _audioSource = GetComponent<AudioSource>();
    }

    public void ShowMore()
    {
        if (_selected == false)
        {
            _selected = true;
            _toolTip.gameObject.SetActive(true);
            _toolTip.DOFade(1, 0.5f);
            transform.DOKill();
            transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);
        }
        else
        {
            _selected = false;
            _toolTip.gameObject.SetActive(false);
            _toolTip.DOFade(0, 0.5f);
            transform.DOKill();
            transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);
        }
        _audioSource.Play();
    }
}
