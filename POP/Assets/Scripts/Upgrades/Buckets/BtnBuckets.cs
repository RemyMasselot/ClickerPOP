using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnBuckets : MonoBehaviour
{
    [SerializeField] private List<GameObject> _contentBuckets = new List<GameObject>();
    [SerializeField] private int _contentBucketsIndex;
    [SerializeField] private Image _imageBtn;
    private Player _player;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void BtnBucket()
    {
        if (_player.WaitCoroutine == false)
        {
            for (int i = 0; i < _contentBuckets.Count; i++)
            {
                if (i == _contentBucketsIndex)
                {
                    _contentBuckets[i].SetActive(true);
                    _player.CheckBucketLimits();
                }
                else
                {
                    _contentBuckets[i].SetActive(false);
                }
            }
            _audioSource.Play();
            _player.UpdateVisualBucketButtons(_imageBtn);
        }
    }
}
