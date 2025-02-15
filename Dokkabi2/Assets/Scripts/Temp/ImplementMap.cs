using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImpelementMap : MonoBehaviour
{
    public CanvasScaler MCanvasScaler;
    
    public Button mBtnCharaEnvironment;
    public Button mBtnMiniGame;
    public Button mBtnUI;
    public Button mBtnGameFlow;

    
    private void Awake()
    {
    }

    private void Start()
    {
        
        mBtnCharaEnvironment.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            GameManager.Instance.Scene.LoadScene(GameData.eScene.CharacterTestScene);
        });
        
        mBtnMiniGame.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            PopupManager.Instance.OpenPopupMiniGameTest(delegate { });
        });
        
        mBtnUI.onClick.AddListener(delegate
        {

            AudioManager.Instance.PlayClick();
            PopupManager.Instance.OpenPopupNotice("개발진행중\n완료예정일 : 3월 1일");

        });
        
        mBtnGameFlow.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            PopupManager.Instance.OpenPopupNotice("개발진행중\n수시 업데이트 예정");

        });
        

    }

}