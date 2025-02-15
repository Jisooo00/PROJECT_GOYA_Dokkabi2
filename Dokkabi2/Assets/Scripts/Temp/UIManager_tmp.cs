using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_tmp : MonoBehaviour
{
    public Button mBtnInterAction;
    
    [SerializeField] private GameObject mActionUI;
    [SerializeField] private UIVirtualJoyStick mJoyStick;
    
    public bool IsJoystickZero
    {
        get
        {
            return mJoyStick.bIsZero;
        }
    }
    
    void Start()
    {
    }

    public void Init()
    {
        mActionUI.SetActive(true);
        mBtnInterAction.transform.parent.gameObject.SetActive(true);
        mJoyStick.mGobJoyStick.gameObject.SetActive(true);
    }

    
    public void DialogKeyOnClick()
    {

    }
    
    public void ForceJoystickPointerUp()
    {
        mJoyStick.ForcePointerUp();
    }
    
    public void ForceJoystickMove(int x,int y)
    {
        if (!mJoyStick.gameObject.activeSelf)
            return;
        mJoyStick.ForceJoystickMove(x,y);
    }


}

