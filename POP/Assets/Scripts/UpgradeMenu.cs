using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _contentUpgradesMachine;
    [SerializeField] private GameObject _contentUpgradesBucket;

    public void BtnBucket()
    {
        _contentUpgradesMachine.SetActive(false);
        _contentUpgradesBucket.SetActive(true);
    }
    public void BtnMachine()
    {
        _contentUpgradesMachine.SetActive(true);
        _contentUpgradesBucket.SetActive(false);
    }
}
