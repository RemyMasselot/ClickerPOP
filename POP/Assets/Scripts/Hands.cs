using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    private PopcornBucket popcornBucket;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private Transform target3;

    private void Awake()
    {
        transform.position = target1.position;
    }

    public void GoToTarget1()
    {
        transform.position = target1.position;
    }

    public void GoToTarget2()
    {
        transform.DOMove(target2.position, 0.2f);
    }

    public void GoToTarget3()
    {
        transform.DOMove(target3.position, 0.15f);
    }
}
