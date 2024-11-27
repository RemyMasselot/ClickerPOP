using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private Player _player;
    private Slider _slider;
    [SerializeField] private TextMeshProUGUI _percentText;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float value)
    {
        for (int i = 0; i < _player.audioSources.Count; i++)
        {
            _player.audioSources[i].volume = value;
        }
        _percentText.text = ((int)(value * 100)).ToString() + "%";
    }
}
