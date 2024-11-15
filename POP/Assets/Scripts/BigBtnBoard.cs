using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BigBtnBoard : MonoBehaviour
{
    public Image ImageBtn;
    public bool Selected = false;
    public TextMeshProUGUI Text;
    public Color Default;
    [SerializeField] private Color _mouseEnter;
    [SerializeField] private Color _mousePress;
    [SerializeField] private List<BigBtnBoard> _buttons = new List<BigBtnBoard>();

    private void Start()
    {
        ImageBtn = GetComponent<Image>();
        if (Selected == true)
        {
            ImageBtn.DOColor(_mousePress, 0.2f);
            Text.DOColor(_mousePress, 0.2f);
        }
    }

    void OnMouseEnter()
    {
        if (gameObject.GetComponent<Button>().interactable == true)
        {
            if (Selected == false)
            {
                _mouseEnter.a = ImageBtn.color.a;
                ImageBtn.DOColor(_mouseEnter, 0.2f);
                Text.DOColor(_mouseEnter, 0.2f);
            }
        }
    }


    void OnMouseDown()
    {
        if (gameObject.GetComponent<Button>().interactable == true)
        {
            Selected = true;
            _mousePress.a = ImageBtn.color.a;
            ImageBtn.DOColor(_mousePress, 0.2f);
            Text.DOColor(_mousePress, 0.2f);
            foreach (var button in _buttons)
            {
                if (button.gameObject.GetComponent<Button>().interactable == true)
                {
                    button.Selected = false;
                    button.Default.a = button.ImageBtn.color.a;
                    button.ImageBtn.DOColor(button.Default, 0.2f);
                    button.Text.DOColor(button.Default, 0.2f);
                }
            }
        }
    }

    void OnMouseExit()
    {
        if (gameObject.GetComponent<Button>().interactable == true)
        {
            if (Selected == false)
            {
                Default.a = ImageBtn.color.a;
                ImageBtn.DOColor(Default, 0.2f);
                Text.DOColor(Default, 0.2f);
            }
        }
    }
}
