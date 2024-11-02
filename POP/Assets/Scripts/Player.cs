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

    public int Money;
    public float MoneyBonus = 1;
    public TextMeshProUGUI TextMoney;
    [SerializeField] private PopcornMachine _popcornMachine;
    [SerializeField] private List<GameObject> popcornBuckets;
    public float TimerAutoclick = 1;
    public int PopNumber = 1;
    public int BucketsSold = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set input actions
        controls = new Controls();
        controls.Enable();
        Click = controls.Player.Click;
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifier si le clic gauche de la souris est enfoncé
        Click.performed += ctx => ExecuteAction();
    }

    private void ExecuteAction()
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
                    }
                    //Debug.Log("CLICK");
                }

                // Si le joueur a appuyé sur un bucket
                if (_popcornMachine.PopcornList.Count > 0)
                {
                    if (popcornBuckets.Contains(hit.collider.gameObject) == true)
                    {
                        PopcornBucket _popcornBucketScript = hit.collider.gameObject.GetComponent<PopcornBucket>();
                        if (_popcornBucketScript.NumberOfPopcornsCurrent < _popcornBucketScript.NumberOfPopcornsLimit)
                        {
                            // Faire pop un popcorn
                            hit.collider.gameObject.GetComponent<PopcornBucket>().FillTheBucket();
                        }
                    }
                }
            }
        }
    }

    public void GainMoney(int bucketPrice)
    {
        Money += (int)(bucketPrice * MoneyBonus);
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
        TextMoney.text = resultat + " €";
    }

    public IEnumerator StartAutoclick()
    {
        _popcornMachine.PopAPopcorn();
        yield return new WaitForSeconds(TimerAutoclick);
        StartCoroutine(StartAutoclick());
    }
}
