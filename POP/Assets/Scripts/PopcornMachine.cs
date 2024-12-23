using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PopcornMachine : MonoBehaviour
{
    private Player _player;
    private BurnPopcorn _burnPopcorn;
    private Stats _stats;
    private Tuto _tuto;

    [SerializeField] private TextMeshProUGUI _tutoCount;
    [SerializeField] private GameObject _popcorn;
    [SerializeField] private Transform _popcornSpawnTarget1;
    [SerializeField] private Transform _popcornSpawnTarget2;
    private Vector3 _target;

    [Header("TRAJECTOIRE")]
    [SerializeField] private float _popcornAngleXmin = -0.5f;
    [SerializeField] private float _popcornAngleXmax = 0.5f;
    [SerializeField] private float _popcornAngleYmin = 0.7f;
    [SerializeField] private float _popcornAngleYmax = 1;
    private Vector2 _popcornAngle;
    [SerializeField] private float _popcornForce = 3;
    [SerializeField] public Transform Pan;
    [SerializeField] private Transform _visual;
    [SerializeField] private Transform _background;

    private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips = new List<AudioClip>();

    public List<GameObject> PopcornList;

    private void Awake()
    {
        _burnPopcorn = FindObjectOfType<BurnPopcorn>();
        _player = FindObjectOfType<Player>();
        _stats = FindObjectOfType<Stats>();
        _tuto = FindObjectOfType<Tuto>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void CickOnMachine()
    {
        if (_burnPopcorn.IsBurning == false)
        {
            // Faire pop un popcorn
            for (int i = 0; i < _player.PopNumber; i++)
            {
                PopAPopcorn();
                Pan.DOKill(true);
                Pan.DOPunchScale(Pan.localScale * 0.15f, 0.5f, 10, 0.8f);
                _visual.DOKill(true);
                _visual.DOPunchScale(_visual.localScale * 0.03f, 0.5f, 10, 0.4f);
                _background.DOKill(true);
                _background.DOPunchScale(_background.localScale * 0.03f, 0.5f, 10, 0.4f);
                _burnPopcorn.transform.DOKill(true);
                _burnPopcorn.transform.DOPunchScale(_burnPopcorn.transform.localScale * 0.03f, 0.5f, 10, 0.4f);
            }
        }
    }

    public void PopAPopcorn()
    {
        // Instancier le popcorn
        float _xPos = Random.Range(_popcornSpawnTarget1.position.x, _popcornSpawnTarget2.position.x);
        _target = new Vector3(_xPos, _popcornSpawnTarget1.position.y, 0);
        GameObject popcorn = Instantiate(_popcorn, _target, Quaternion.identity);
        
        // Ajouter le popcorn instanci� � la liste de popcorns contenus dans la Popcorn Machine
        PopcornList.Add(popcorn);

        // Donner une trajectoire random au popcorn instanci�
        Rigidbody2D _rb = popcorn.GetComponent<Rigidbody2D>();
        float _x = Random.Range(_popcornAngleXmin, _popcornAngleXmax);
        float _y = Random.Range(_popcornAngleYmin, _popcornAngleYmax);
        _popcornAngle = new Vector2(_x, _y);
        _rb.AddForce(_popcornAngle * _popcornForce, ForceMode2D.Impulse);
        //Debug.Log("POPCORN");

        //Play son
        int randomIndex = Random.Range(0, _audioClips.Count);
        _audioSource.clip = _audioClips[randomIndex];

        _audioSource.Play();

        _stats.TotalPopcorn++;

        //Tuto
        if(_tuto.TutoIsDone == false && _tuto.Part == 0)
        {
            _tutoCount.text = PopcornList.Count.ToString();
            _tuto.transform.DOKill(true);
            _tuto.transform.DOPunchScale(_tuto.transform.localScale * 0.05f, 0.5f, 8, 1f);

            if (PopcornList.Count == 10)
            {
                _tuto.ObjMachineDone();
            }
        }
    }
}
