using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PopAnims : MonoBehaviour
{
    private Animator _animatorBase;

    private void Awake()
    {
        _animatorBase = GetComponent<Animator>();
    }

    public void UpdateAnim(string boolTrue, string boolFalse1, string boolFalse2)
    {
        _animatorBase.SetBool(boolTrue, true);
        _animatorBase.SetBool(boolFalse1, false);
        _animatorBase.SetBool(boolFalse2, false);
    }
}
