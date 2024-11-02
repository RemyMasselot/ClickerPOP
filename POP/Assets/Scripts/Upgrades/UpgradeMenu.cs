using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _contentUpgradesMachine;
    [SerializeField] private GameObject _contentUpgradesBucket;
    [SerializeField] private Button _button;

    private void Start()
    {
        if (_button != null)
        {
            _button.interactable = false;
        }
    }

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
