using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBuckets : MonoBehaviour
{
    [SerializeField] private List<GameObject> _contentBuckets = new List<GameObject>();
    [SerializeField] private int _contentBucketsIndex;

    public void BtnBucket()
    {
        for (int i = 0; i < _contentBuckets.Count - 1; i++)
        {
            if (i == _contentBucketsIndex)
            {
                _contentBuckets[i].SetActive(true);
            }
            else
            {
                _contentBuckets[i].SetActive(false);
            }
        }
    }
}
