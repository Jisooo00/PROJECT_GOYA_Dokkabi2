using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupMiniGameTest : UIPopup
{
    // Start is called before the first frame update
    public Button m_btnClose;
    public Button m_btnMiniGame1;
    public Button m_btnMiniGame2;
    public Button m_btnMiniGame3;
    public Button m_btnMiniGame4;



    void Start()
    {
    }

    public void Init(Action del, string msg, string title = "", bool bNoBtn = false)
    {
        base.Init(del);
        

        m_btnClose.gameObject.SetActive(bNoBtn);
        m_btnClose.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            PopupManager.Instance.SetClosePopup();
        });
        
        m_btnMiniGame1.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            GameManager.Instance.Scene.LoadScene(GameData.eScene.GameTestScene_1);
        });
        
        m_btnMiniGame2.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            PopupManager.Instance.OpenPopupNotice("개발진행중\n완료예정일 : 2월 19일");
        });
        
        m_btnMiniGame3.onClick.AddListener(delegate
        {

            AudioManager.Instance.PlayClick();
            PopupManager.Instance.OpenPopupNotice("개발진행중\n완료예정일 : 2월 22일");

        });
        
        m_btnMiniGame4.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            PopupManager.Instance.OpenPopupNotice("개발진행중\n완료예정일 : 2월 25일");

        });
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}

