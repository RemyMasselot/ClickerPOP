using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Text;
using DG.Tweening;

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
                                Transform visual = _popcornBucketScript.transform.GetChild(0).GetComponent<Transform>();
                                visual.DOKill(true);
                                visual.DOPunchScale(visual.localScale * 0.2f, 0.5f, 10, 0);
                            }
                        }
                    }
                }
            }
        }
    }

    public void UpdateMoney()
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
}
