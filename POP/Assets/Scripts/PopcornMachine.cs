using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PopcornMachine : MonoBehaviour
{
    [SerializeField] private GameObject popcorn;
    [SerializeField] private Transform PopcornSpawnTarget1;
    [SerializeField] private Transform PopcornSpawnTarget2;
    private Vector3 target;

    [Header("TRAJECTOIRE")]
    [SerializeField] private float popcornAngleXmin = -0.5f;
    [SerializeField] private float popcornAngleXmax = 0.5f;
    [SerializeField] private float popcornAngleYmin = 0.7f;
    [SerializeField] private float popcornAngleYmax = 1;
    private Vector2 popcornAngle;
    [SerializeField] private float popcornForce = 3;

    [Header("TRAJECTOIRE")]
    public List<GameObject> PopcornList;


    public void PopAPopcorn()
    {
        // Instancier le popcorn
        float _xPos = Random.Range(PopcornSpawnTarget1.position.x, PopcornSpawnTarget2.position.x);
        target = new Vector3(_xPos, PopcornSpawnTarget1.position.y, 0);
        GameObject _popcorn = Instantiate(popcorn, target, Quaternion.identity);
        
        // Ajouter le popcorn instancié à la liste de popcorns contenus dans la Popcorn Machine
        PopcornList.Add(_popcorn);

        // Donner une trajectoire random au popcorn instancié
        Rigidbody2D _rb = _popcorn.GetComponent<Rigidbody2D>();
        float _x = Random.Range(popcornAngleXmin, popcornAngleXmax);
        float _y = Random.Range(popcornAngleYmin, popcornAngleYmax);
        popcornAngle = new Vector2(_x, _y);
        _rb.AddForce(popcornAngle * popcornForce, ForceMode2D.Impulse);
        //Debug.Log("POPCORN");
    }
}
