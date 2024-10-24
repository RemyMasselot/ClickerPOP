using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopcornBucket : MonoBehaviour
{
    public PopcornMachine PopcornMachine;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> Sprites = new List<Sprite>();

    public int NumberOfPopcornsCurrent = 0;
    public int NumberOfPopcornsLimit = 10;

    [SerializeField] private Hands handLeft;
    [SerializeField] private Hands handRight;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FillTheBucket()
    {
        // Supprimer le popcorn de la liste des popcorn contenus dans la Popcorn Machine
        GameObject _popcornRemoved = PopcornMachine.PopcornList[PopcornMachine.PopcornList.Count - 1];
        PopcornMachine.PopcornList.RemoveAt(PopcornMachine.PopcornList.Count - 1);
        // Détruire ce popcorn
        Destroy(_popcornRemoved);

        NumberOfPopcornsCurrent ++;

        // Mettre a jour le sprite du bucket selon le nombre de popcorns contenus
        if (NumberOfPopcornsCurrent == NumberOfPopcornsLimit)
        {
            spriteRenderer.sprite = Sprites[3];
            handLeft.GoToTarget2();
            handRight.GoToTarget2();
        }
        if (NumberOfPopcornsCurrent <= NumberOfPopcornsLimit / 1.5f)
        {
            spriteRenderer.sprite = Sprites[2];
        }
        if (NumberOfPopcornsCurrent <= NumberOfPopcornsLimit / 2)
        {
            spriteRenderer.sprite = Sprites[1];
        }
        if (NumberOfPopcornsCurrent <= NumberOfPopcornsLimit / 4)
        {
            spriteRenderer.sprite = Sprites[0];
        }

        if (NumberOfPopcornsCurrent == NumberOfPopcornsLimit)
        {
            //ChangeTheBucket();
        }
    }

    private void ChangeTheBucket()
    {

    }
}
