using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerHover : MonoBehaviour
{
    public GameObject _hoverVisibleObj;
        
    public void OnPointerEnter()
    {
        _hoverVisibleObj?.SetActive(true);
    }

    public void OnPointerExit()
    {
        _hoverVisibleObj?.SetActive(false);
    }
}
