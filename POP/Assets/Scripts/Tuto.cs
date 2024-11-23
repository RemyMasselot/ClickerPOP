using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    private CanvasGroup _canvaGroup;
    private Sentences _sentences;
    public bool TutoIsDone = false;
    private AudioSource _audioSource;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private GameObject _buckets;
    [SerializeField] private GameObject _textCount1;
    [SerializeField] private GameObject _textCount2;
    [SerializeField] private TextMeshProUGUI _tutoDesc;
    [SerializeField] private Color _finish;
    [SerializeField] private RawImage _box;

    public int Part = 0;

    private void Awake()
    {
        _canvaGroup = GetComponent<CanvasGroup>();
        _audioSource = GetComponent<AudioSource>();
        _sentences = FindObjectOfType<Sentences>();
        _tutoDesc.text = _sentences.TutoTextsEN[Part];
    }

    public void ObjMachineDone()
    {
        if (Part == 0)
        {
            _audioSource.Play();
            transform.DOKill(true);
            transform.DOPunchScale(transform.localScale * 0.25f, 0.5f, 8, 0.8f);
            _canvaGroup.DOFade(0, 1f)
                .OnComplete(() =>
                {
                    transform.localPosition = _endPosition;
                    _textCount1.SetActive(false);
                    _textCount2.SetActive(false);
                    Part++;
                    if (_sentences.IsFrench == true)
                    {
                        _tutoDesc.text = _sentences.TutoTextsFR[Part];
                    }
                    else
                    {
                        _tutoDesc.text = _sentences.TutoTextsEN[Part];
                    }
                    //_tutoDesc.text = "Appuie sur le pot disponible pour le remplir";
                    DOVirtual.DelayedCall(0.2f, () =>
                    {
                        transform.DOKill(true);
                        transform.DOPunchScale(transform.localScale * 0.05f, 0.5f, 8, 0.8f);
                        _canvaGroup.DOFade(1, 1f);
                        _buckets.SetActive(true);
                    });
                });
        }
    }

    public void ObjBucketDone()
    {
        if (Part == 1)
        {
            Part++;
            if (_sentences.IsFrench == true)
            {
                _tutoDesc.text = _sentences.TutoTextsFR[Part];
            }
            else
            {
                _tutoDesc.text = _sentences.TutoTextsEN[Part];
            }
            _tutoDesc.color = _finish;
            _box.color = _finish;
            _audioSource.Play();
            transform.DOKill(true);
            transform.DOPunchScale(transform.localScale * 0.25f, 0.5f, 8, 0.8f);
            DOVirtual.DelayedCall(3f, () =>
            {
                Part++;
                if (_sentences.IsFrench == true)
                {
                    _tutoDesc.text = _sentences.TutoTextsFR[Part];
                }
                else
                {
                    _tutoDesc.text = _sentences.TutoTextsEN[Part];
                }
                transform.DOKill(true);
                transform.DOPunchScale(transform.localScale * 0.1f, 0.5f, 6, 1f);
                DOVirtual.DelayedCall(2f, () =>
                {
                    _canvaGroup.DOFade(0, 1f)
                    .OnComplete(() =>
                    {
                        TutoIsDone = true;
                        gameObject.SetActive(false);
                    });
                });
            });
        }
    }
}
