using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BtnLanguage : MonoBehaviour
{
    private Sentences _sentences;
    private Image _image;
    private TextMeshProUGUI _text;
    private AudioSource _audioSource;
    [SerializeField] private Image _imageOther;
    [SerializeField] private TextMeshProUGUI _textOther;
    [SerializeField] private Color _default;
    [SerializeField] private Color _isSelected;

    private void Awake()
    {
        _sentences = FindObjectOfType<Sentences>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnEnglish()
    {
        _sentences.IsFrench = false;
        _image.color = _isSelected;
        _text.color = _isSelected;
        _imageOther.color = _default;
        _textOther.color = _default;
        _audioSource.Play();
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);
        _sentences.SetEnglish();
    }

    public void OnFrench()
    {
        _sentences.IsFrench = true;
        _image.color = _isSelected;
        _text.color = _isSelected;
        _imageOther.color = _default;
        _textOther.color = _default;
        _audioSource.Play();
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);
        _sentences.SetFrench();
    }
}
