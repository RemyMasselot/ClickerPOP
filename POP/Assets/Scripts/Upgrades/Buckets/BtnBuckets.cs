using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BtnBuckets : MonoBehaviour
{
    [SerializeField] private List<GameObject> _contentBuckets = new List<GameObject>();
    [SerializeField] private int _contentBucketsIndex;
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    public void BtnBucket()
    {
        for (int i = 0; i < _contentBuckets.Count - 1; i++)
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
    }
}
