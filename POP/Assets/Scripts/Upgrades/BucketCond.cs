using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BucketCond : MonoBehaviour
{
    public int NumBucketsToSell;
    private Button _button;
    private Player _player;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _button = GetComponentInParent<Button>();
        _button.interactable = false;
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = _player.BucketsSold + "/" + NumBucketsToSell.ToString();
    }

    public void CheckNumBuckets()
    {
        _text.text = _player.BucketsSold + "/" + NumBucketsToSell.ToString();
        if (_player.BucketsSold >= NumBucketsToSell)
        {
            _button.interactable = true;
            gameObject.SetActive(false);
        }
    }
}
