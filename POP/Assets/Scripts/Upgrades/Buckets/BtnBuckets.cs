using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnBuckets : MonoBehaviour
{
    [SerializeField] private List<GameObject> _contentBuckets = new List<GameObject>();
    [SerializeField] private int _contentBucketsIndex;
    [SerializeField] private Image _imageBtn;
    public Color DefaultNumColor;
    public Color SelectedNumColor;

    private List<Scrollbar> _scrollbars = new List<Scrollbar>();
    public List<TextMeshProUGUI> BucketNums = new List<TextMeshProUGUI>();
    private float _currentValue;

    private Player _player;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _audioSource = GetComponent<AudioSource>();
        foreach (var item in _contentBuckets)
        {
            _scrollbars.Add(item.GetComponentInChildren<Scrollbar>());
        }
    }

    public void OnBtnBucket()
    {
        if (_player.WaitCoroutine == false)
        {
            for (int i = 0; i < _contentBuckets.Count; i++)
            {
                if (_contentBuckets[i].activeSelf == true)
                {
                    _currentValue = _scrollbars[i].value;
                }
            }

            for (int i = 0; i < _contentBuckets.Count; i++)
            {
                if (i == _contentBucketsIndex)
                {
                    _contentBuckets[i].SetActive(true);
                    _scrollbars[i].value = _currentValue;
                    _player.CheckBucketLimits();
                    BucketNums[i].fontSize = 30;
                    BucketNums[i].color = SelectedNumColor;
                }
                else
                {
                    _contentBuckets[i].SetActive(false);
                    BucketNums[i].fontSize = 22;
                    BucketNums[i].color = DefaultNumColor;
                }
            }
            _audioSource.Play();
            _player.UpdateVisualBucketButtons(_imageBtn);
        }
    }
}
