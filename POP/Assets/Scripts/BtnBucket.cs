using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBucket : MonoBehaviour
{
    [SerializeField] private PopcornBucket _popcornBucket;

    public void OnBcuket()
    {
        if ( _popcornBucket.gameObject.activeSelf == true)
        {
            _popcornBucket.CickOnBucket();
        }
        Debug.Log("OnBucket");
    }
}
