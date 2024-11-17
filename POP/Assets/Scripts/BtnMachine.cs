using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMachine : MonoBehaviour
{
    private PopcornMachine _popcornMachine;

    private void Awake()
    {
        _popcornMachine = FindObjectOfType<PopcornMachine>();
    }

    public void OnMachine()
    {
        _popcornMachine.CickOnMachine();
    }
}
