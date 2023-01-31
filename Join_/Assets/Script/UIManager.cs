using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject readyUI;
    [SerializeField] Button[] buttons;

    private void Start() 
    {
        for (int i = 0; i < buttons.Length; ++i)
            buttons[i].interactable = false;
    }

    public void readyUIOff()
    {
        readyUI.SetActive(false);
    }
}
