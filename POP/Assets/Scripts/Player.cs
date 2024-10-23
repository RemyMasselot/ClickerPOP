using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Controls controls;
    public InputAction Click;

    [SerializeField] private GameObject popcornMachine;
    //[SerializeField] private GameObject popcornBucket;

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
        // V�rifier si le clic gauche de la souris est enfonc�
        Click.performed += ctx => ExecuteAction();
    }

    private void ExecuteAction()
    {
        // Convertir la position de la souris de l'�cran � l'espace monde
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Lancer un Raycast � la position de la souris
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // Si le joueur a appuy� sur un object
        if (hit.collider != null)
        {
            // Si le joueur a appuy� sur la Popcorn Machine
            if (hit.collider.gameObject == popcornMachine)
            {
                // Faire pop un popcorn
                popcornMachine.GetComponent<PopcornMachine>().PopAPopcorn();
                Debug.Log("CLICK");
            }

            // Si le joueur a appuy� sur une Popcorn Bucket
            //if (hit.collider.gameObject == popcornBucket)
            //{
            //    // Faire pop un popcorn
            //    popcornMachine.GetComponent<PopcornMachine>().PopAPopcorn();
            //    Debug.Log("CLICK");
            //}
        }
    }
}
