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

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    public void BtnBucket()
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
        _player.UpdateVisualBucketButtons(_imageBtn);
    }
}
