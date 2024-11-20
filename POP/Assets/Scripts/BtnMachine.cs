using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnMachine : MonoBehaviour
{
    private PopcornMachine _popcornMachine;
    private Tuto _tuto;

    private void Awake()
    {
        _popcornMachine = FindObjectOfType<PopcornMachine>();
        _tuto = FindObjectOfType<Tuto>();
    }

    public void OnMachine()
    {
        if (_tuto.TutoIsDone == true || (_tuto.TutoIsDone == false & _popcornMachine.PopcornList.Count < 10))
        {
            _popcornMachine.CickOnMachine();
        }
    }
}
