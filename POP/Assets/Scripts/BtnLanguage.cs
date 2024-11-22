using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnLanguage : MonoBehaviour
{
    private Sentences _sentences;
    private Image _image;
    private TextMeshProUGUI _text;
    [SerializeField] private Image _imageOther;
    [SerializeField] private TextMeshProUGUI _textOther;
    [SerializeField] private Color _default;
    [SerializeField] private Color _isSelected;

    private void Awake()
    {
        _sentences = FindObjectOfType<Sentences>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnEnglish()
    {
        _sentences.IsFrench = false;
        _image.color = _isSelected;
        _text.color = _isSelected;
        _imageOther.color = _default;
        _textOther.color = _default;
        _sentences.SetEnglish();
    }

    public void OnFrench()
    {
        _sentences.IsFrench = true;
        _image.color = _isSelected;
        _text.color = _isSelected;
        _imageOther.color = _default;
        _textOther.color = _default;
        _sentences.SetFrench();
    }
}
