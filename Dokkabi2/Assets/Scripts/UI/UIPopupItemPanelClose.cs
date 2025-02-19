using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItemPanelClose : UIPopupItemPanel
{ 
    public void Init()
    {
    }

    public void OnClickTitle()
    {
        GameManager.Instance.Scene.LoadScene(GameData.eScene.IntroScene);
    }

    public void OnClickCloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}