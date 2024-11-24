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
    [SerializeField] private BtnBuckets _btnBucket;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioDefault;
    [SerializeField] private List<TextMeshProUGUI> _bucketNums = new List<TextMeshProUGUI>();
    [SerializeField] private List<GameObject> _buckets = new List<GameObject>();

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
                if (_buckets[i].activeSelf == true)
                {
                    _bucketNums[i].fontSize = 30;
                    _bucketNums[i].color = _btnBucket.SelectedNumColor;
                }
                else
                {
                    _bucketNums[i].fontSize = 22;
                    _bucketNums[i].color = _btnBucket.DefaultNumColor;
                }
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
            for (int i = 0; i < _btnBucket.BucketNums.Count; i++)
            {
                _bucketNums[i].fontSize = 22;
                _bucketNums[i].color = _btnBucket.DefaultNumColor;
            }
            _contentUpgradesBucket.SetActive(false);
            _player.CheckBucketLimits();
            _player.UpdateVisualMainButtons(_imageBtn);
            _audioSource.Play();
        }
    }
}
