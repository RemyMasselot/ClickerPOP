using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _contentUpgradesMachine;
    [SerializeField] private Scrollbar _MachineScrollbar;
    [SerializeField] private List<Scrollbar> _bucektScrollbars = new List<Scrollbar>();
    [SerializeField] private GameObject _contentUpgradesBucket;
    [SerializeField] private Image _imageBtn;
    private Player _player;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioDefault;

    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void BtnBucket()
    {
        if (_player.WaitCoroutine == false)
        {
            _contentUpgradesMachine.SetActive(false);
            _contentUpgradesBucket.SetActive(true);
            for (int i = 0; i < _bucektScrollbars.Count; i++)
            {
                _bucektScrollbars[i].value = 1;
            }
            _player.CheckBucketLimits();
            _player.UpdateVisualMainButtons(_imageBtn);
            _audioSource.clip = _audioDefault;
            _audioSource.Play();
        }
    }
    public void BtnMachine()
    {
        if (_player.WaitCoroutine == false)
        {
            _contentUpgradesMachine.SetActive(true);
            _MachineScrollbar.value = 1;
            _contentUpgradesBucket.SetActive(false);
            _player.CheckBucketLimits();
            _player.UpdateVisualMainButtons(_imageBtn);
            _audioSource.Play();
        }
    }
}
