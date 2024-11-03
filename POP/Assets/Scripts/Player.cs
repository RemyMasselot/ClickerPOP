using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Text;

public class Player : MonoBehaviour
{
    Controls controls;
    public InputAction Click;

    private PopcornMachine _popcornMachine;
    private BurnPopcorn _burnPopcorn;

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
        // V�rifier si le clic gauche de la souris est enfonc�
        Click.performed += ctx => ExecuteAction();
    }

    private void ExecuteAction()
    {
        // Convertir la position de la souris de l'�cran � l'espace monde
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Lancer un Raycast � la position de la souris
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        // Si le joueur a appuy� sur un object
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                // Si le joueur a appuy� sur la Popcorn Machine
                if (hit.collider.gameObject.layer == 3)
                {
                    // Faire pop un popcorn
                    for (int i = 0; i < PopNumber; i++)
                    {
                        _popcornMachine.PopAPopcorn();
                    }
                    //Debug.Log("CLICK");
                }

                // Si le joueur a appuy� sur un bucket
                if (_popcornMachine.PopcornList.Count > 0)
                {
                    if (PopcornBuckets.Contains(hit.collider.gameObject) == true)
                    {
                        PopcornBucket _popcornBucketScript = hit.collider.gameObject.GetComponent<PopcornBucket>();
                        if (_popcornBucketScript.NumberOfPopcornsCurrent < _popcornBucketScript.NumberOfPopcornsLimit)
                        {
                            // Faire pop un popcorn
                            hit.collider.gameObject.GetComponent<PopcornBucket>().RepeatFillTheBucket();
                        }
                    }
                }
            }
        }
    }

    public void GainMoney(int bucketPrice)
    {
        Money += bucketPrice;
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
        TextMoney.text = resultat + " �";
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

    public IEnumerator StartAutoclickBucket(int index, float timer)
    {
        while (_burnPopcorn.IsBurning == true)
        {
            yield return null;
        }
        if (_popcornMachine.PopcornList.Count > 0)
        {
            PopcornBucket _bucket = PopcornBuckets[index].GetComponent<PopcornBucket>();
            if (_bucket.NumberOfPopcornsCurrent < _bucket.NumberOfPopcornsLimit)
            {
                _bucket.FillTheBucket();
            }
        }
        yield return new WaitForSeconds(timer);
        StartCoroutine(StartAutoclickBucket(index, timer));
    }
}
