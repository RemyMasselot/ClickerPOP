using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BtnPhysic : MonoBehaviour
{
    private Player _player;
    private PopcornMachine _machine;
    private Image _image;

    [SerializeField] private Sprite _used;
    [SerializeField] private Sprite _notUsed;
    private AudioSource _audioSource;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _machine = FindObjectOfType<PopcornMachine>();
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void UsePhysic()
    {
        if (_player.UsePhysic == false)
        {
            _player.UsePhysic = true;
            for (int i = 0; i < _machine.PopcornList.Count - 1; i++)
            {
                Rigidbody2D rb = _machine.PopcornList[i].GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.Sleep();
            }
            _image.sprite = _used;
        }
        else
        {
            _player.UsePhysic = false;
            for (int i = 0; i < _machine.PopcornList.Count - 1; i++)
            {
                Popcorn popcorn = _machine.PopcornList[i].GetComponent<Popcorn>();
                if (popcorn.CanBurn == true)
                {
                    Rigidbody2D rb = _machine.PopcornList[i].GetComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.angularVelocity = 0;
                }
            }
            _image.sprite = _notUsed;
        }
        _audioSource.Play();
        transform.DOKill(true);
        transform.DOPunchScale(transform.localScale * 0.2f, 0.5f);
    }
}
