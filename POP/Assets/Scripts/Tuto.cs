using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Tuto : MonoBehaviour
{
    private CanvasGroup _canvaGroup;
    public bool TutoIsDone = false;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private GameObject _buckets;
    [SerializeField] private GameObject _textCount1;
    [SerializeField] private GameObject _textCount2;

    [SerializeField] private TextMeshProUGUI _tutoDesc;

    private void Awake()
    {
        _canvaGroup = GetComponent<CanvasGroup>();
    }

    public void ObjMachineDone()
    {
        transform.DOPunchScale(transform.localScale * 0.1f, 0.5f, 8, 0.8f);
        _canvaGroup.DOFade(0, 1f)
            .OnComplete(() =>
            {
                transform.localPosition = _endPosition;
                _textCount1.SetActive(false);
                _textCount2.SetActive(false);
                _tutoDesc.text = "Appuie sur le pot disponible pour le remplir";
                DOVirtual.DelayedCall(0.2f, () =>
                {
                    transform.DOPunchScale(transform.localScale * 0.05f, 0.5f, 8, 0.8f);
                    _canvaGroup.DOFade(1, 1f)
                    .OnComplete(() =>
                    {
                        _buckets.SetActive(true);
                    });
                });
            });
    }

    public void ObjBucketDone()
    {
        _tutoDesc.text = "Bravo ! Tu as vendu ton premier pot !";
        transform.DOPunchScale(transform.localScale * 0.1f, 0.5f, 8, 0.8f);
        DOVirtual.DelayedCall(3f, () =>
        {
            _tutoDesc.text = "C'est toi le chef maintenant !";
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
