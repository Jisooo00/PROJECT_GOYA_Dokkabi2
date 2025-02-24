using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItemPanelClose : UIPopupItemPanel
{ 
    public override void Init()
    {
    }

    public void OnClickTitle()
    {
        AudioManager.Instance.PlayClick();
        GameManager.Instance.Scene.LoadScene(GameData.eScene.IntroScene);
    }

    public void OnClickCloseGame()
    {
        AudioManager.Instance.PlayClick();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}