using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpdateCursor : MonoBehaviour
{

    private DefaultInputActions _defaultInputActions;
    public InputAction Click;
    [SerializeField] private Texture2D _textureOn;

    private void Awake()
    {
        // Set input actions
        _defaultInputActions = new DefaultInputActions();
        _defaultInputActions.Enable();
        Click = _defaultInputActions.UI.Click;
    }

    private void Update()
    {
        Click.performed += ctx => ChangeCursor();
        Click.canceled += ctx => ResetCursor();
    }

    private void ChangeCursor()
    {
        Cursor.SetCursor(_textureOn, Vector2.zero, CursorMode.Auto);
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
