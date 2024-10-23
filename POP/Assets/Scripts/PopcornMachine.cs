using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornMachine : MonoBehaviour
{
    [SerializeField] GameObject popcorn;
    [SerializeField] Transform target;
    [SerializeField] Vector2 popcornSpawn;

    public void PopAPopcorn()
    {
        Instantiate(popcorn, target.position, Quaternion.identity);

        Debug.Log("POPCORN");
    }
}
