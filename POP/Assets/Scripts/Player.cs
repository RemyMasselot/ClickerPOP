using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Text;
using DG.Tweening;
using UnityEngine.UI;
using static UnityEditor.Progress;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class Player : MonoBehaviour
{
    Controls controls;
    public InputAction Click;

    private PopcornMachine _popcornMachine;
    private BurnPopcorn _burnPopcorn;

    public bool ShieldActivated = false;
    public int Money;
    public TextMeshProUGUI TextMoney;
    public List<GameObject> PopcornBuckets;
    public float TimerAutoclick = 1;
    public int PopNumber = 1;
    public int BucketsSold = 0;
    public float ClientTips = 1;
    public float BucketPriceDivider = 4;

    public Color Default;
    public Color NoColor;
    [SerializeField] private Color _mouseEnter;
    [SerializeField] private Color _selected;
    [SerializeField] private Color _mousePressCanBuy;
    [SerializeField] private Color _mousePressCantBuy;
    [SerializeField] private List<Image> _MainButtons = new List<Image>();
    [SerializeField] private List<Image> _BucketButtons = new List<Image>();

    // Start is called before the first frame update
    void Awake()
    {
        // Set input actions
        controls = new Controls();
        controls.Enable();
        Click = controls.Player.Click;

        _popcornMachine = FindObjectOfType<PopcornMachine>();
        _burnPopcorn = FindObjectOfType<BurnPopcorn>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier si le clic gauche de la souris est enfoncé
        Click.performed += ctx => ExecuteAction();
    }

    private void ExecuteAction()
    {
        if (_burnPopcorn.IsBurning == false)
        {
            // Convertir la position de la souris de l'écran à l'espace monde
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Lancer un Raycast à la position de la souris
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            // Si le joueur a appuyé sur un object
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    // Si le joueur a appuyé sur la Popcorn Machine
                    if (hit.collider.gameObject.layer == 3)
                    {
                        // Faire pop un popcorn
                        for (int i = 0; i < PopNumber; i++)
                        {
                            _popcornMachine.PopAPopcorn();
                            PopcornMachine _popcornMachineScript = hit.collider.gameObject.GetComponent<PopcornMachine>();
                            Transform visual = _popcornMachineScript.Pan;
                            visual.DOKill(true);
                            visual.DOPunchScale(visual.localScale * 0.2f, 0.5f, 10, 0);
                        }
                        //Debug.Log("CLICK");
                    }

                    // Si le joueur a appuyé sur un bucket
                    if (_popcornMachine.PopcornList.Count > 0)
                    {
                        if (PopcornBuckets.Contains(hit.collider.gameObject) == true)
                        {
                            PopcornBucket _popcornBucketScript = hit.collider.gameObject.GetComponent<PopcornBucket>();
                            if (_popcornBucketScript.NumberOfPopcornsCurrent < _popcornBucketScript.NumberOfPopcornsLimit)
                            {
                                // Faire pop un popcorn
                                _popcornBucketScript.RepeatFillTheBucket(_popcornBucketScript.FillNumber);
                                Transform visualShadow = _popcornBucketScript.transform.GetChild(0).GetComponent<Transform>();
                                Transform visualBucket = _popcornBucketScript.transform.GetChild(1).GetComponent<Transform>();
                                visualShadow.DOKill(true);
                                visualShadow.DOPunchScale(visualShadow.localScale * 0.5f, 0.5f, 10, 0);
                                visualBucket.DOKill(true);
                                visualBucket.DOPunchScale(visualBucket.localScale * 0.2f, 0.5f, 10, 0);
                            }
                        }
                    }
                }
            }
        }
    }

    public void UpdateMoney(bool fromUpgrade)
    {
        string money = Money.ToString();
        StringBuilder resultat = new StringBuilder();
        for (int i = 0; i < money.Length; i++)
        {
            if (i > 0 && i % 3 == 0)
            {
                resultat.Insert(0, '.');
            }
            resultat.Insert(0, money[money.Length - 1 - i]);
        }
        TextMoney.text = "$" + resultat;
        if (fromUpgrade == true)
        {
            TextMoney.gameObject.GetComponent<Transform>().DOPunchScale(transform.localScale * 0.05f, 0.3f, 8, 0);
        }
        else
        {
            TextMoney.gameObject.GetComponent<Transform>().DOPunchScale(transform.localScale * -0.1f, 0.5f, 6, 0.4f);
        }
    }

    public void CheckBucketLimits()
    {
        BucketCond[] obj = FindObjectsOfType<BucketCond>();
        foreach (BucketCond item in obj)
        {
            item.CheckNumBuckets();
        }
    }

    public IEnumerator StartAutoclickMachine()
    {
        while (_burnPopcorn.IsBurning == true)
        {
            yield return null;
        }
        _popcornMachine.PopAPopcorn();
        yield return new WaitForSeconds(TimerAutoclick);
        StartCoroutine(StartAutoclickMachine());
    }

    //Visual buttons
    public void UpdateVisualCanBuy(Transform transform, Image image)
    {
        transform.DOKill(true);
        image.DOColor(_mousePressCanBuy, 0.2f);
        transform.DOPunchScale(transform.localScale * -0.2f, 0.5f, 10, 0)
        .OnComplete(() =>
        {
            image.DOColor(NoColor, 0.2f);
            });
    }

    public void UpdateVisualCantBuy(Transform transform, Image image)
    {
        transform.DOKill(true);
        image.DOColor(_mousePressCantBuy, 0.2f);
        transform.DOPunchScale(transform.localScale * -0.1f, 0.5f, 10, 0)
        .OnComplete(() =>
        {
            image.DOColor(NoColor, 0.2f);
            });
    }

    public void UpdateVisualMainButtons(Image image)
    {
        TextMeshProUGUI text = new();
        foreach (var item in _MainButtons)
        {
            if (item.gameObject.GetComponent<Button>().interactable == true)
            {
                item.color = Default;
                text = item.gameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                text.color = Default;
            }
        }
        image.gameObject.transform.DOKill(true);
        image.DOColor(_selected, 0.2f);
        text = image.gameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.DOColor(_selected, 0.2f);
        image.gameObject.transform.DOPunchScale(image.gameObject.transform.localScale * -0.1f, 0.5f, 10, 0);
    }

    public void UpdateVisualBucketButtons(Image image)
    {
        TextMeshProUGUI text = new();
        foreach (var item in _BucketButtons)
        {
            if (item.gameObject.GetComponent<Button>().interactable == true)
            {
                item.color = Default;
                text = item.gameObject.GetComponentInChildren<TextMeshProUGUI>();
                text.color = Default;
            }
        }
        image.gameObject.transform.DOKill(true);
        image.DOColor(_selected, 0.2f);
        text = image.gameObject.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.DOColor(_selected, 0.2f);
        image.gameObject.transform.DOPunchScale(image.gameObject.transform.localScale * -0.1f, 0.5f, 10, 0);
    }
}
