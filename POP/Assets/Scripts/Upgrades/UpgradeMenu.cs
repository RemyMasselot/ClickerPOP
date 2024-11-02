using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _contentUpgradesMachine;
    [SerializeField] private GameObject _contentUpgradesBucket;
    private Player _player;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    public void BtnBucket()
    {
        _contentUpgradesMachine.SetActive(false);
        _contentUpgradesBucket.SetActive(true);
        _player.CheckBucketLimits();
    }
    public void BtnMachine()
    {
        _contentUpgradesMachine.SetActive(true);
        _contentUpgradesBucket.SetActive(false);
        _player.CheckBucketLimits();
    }
}