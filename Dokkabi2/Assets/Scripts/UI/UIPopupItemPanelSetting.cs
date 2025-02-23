using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItemPanelSetting : UIPopupItemPanel
{
    public CheckButton mBgmBtn;
    public CheckButton mEffectBtn;
    public Slider mSliderVol;
    public CheckButton mInteractionBtn;
    public CheckButton mJoyStickBtn;
    public Slider mSliderCam;
    public TMPro.TMP_Text m_textSetCam;
    public TMPro.TMP_Text m_textVersion;
    
    [Serializable]
    public class CheckButton
    {
        public Button m_btn;
        public bool IsChecked;
        public GameObject mGOChecked;

        public void Init(bool isChecked)
        {
            IsChecked = isChecked;
            SetChecked();
            m_btn.onClick.AddListener(delegate
            {
                IsChecked = !IsChecked;
                SetChecked();
            });
        }

        public void SetChecked()
        {
            mGOChecked.SetActive(IsChecked);
        }

    }

    void Start()
    {
        mBgmBtn.Init(GameData.myData.IS_BGM_ON);
        mEffectBtn.Init(GameData.myData.IS_EFFECT_ON);
        mSliderVol.value = GameData.myData.SET_VOLUME;
        mSliderCam.value = GameData.myData.SET_CAM;
        mInteractionBtn.Init(GameData.myData.IS_SHOW_UI_BTN);
        mJoyStickBtn.Init(GameData.myData.IS_HIDE_JOYSTICK);
        m_textVersion.text = String.Format("VERSION {0}",Application.version);
    }

    public override void Init()
    {
        mSliderVol.onValueChanged.AddListener(delegate { AudioManager.Instance.SetVolumeCheck(mSliderVol.value); });
        mSliderCam.onValueChanged.AddListener(delegate { m_textSetCam.text = mSliderCam.value.ToString();});
    }

    public void OnSet()
    {
        if (GameData.myData.IS_BGM_ON != mBgmBtn.IsChecked)
        {
            GameData.myData.SetBgmOn(mBgmBtn.IsChecked);
            if (!mBgmBtn.IsChecked)
            {
                AudioManager.Instance.StopBgm();
            }
            else
            {
                AudioManager.Instance.PlayBgm();
            }
        }

        if (GameData.myData.IS_EFFECT_ON != mEffectBtn.IsChecked)
        {
            GameData.myData.SetEffectOn(mEffectBtn.IsChecked);
        }

        if (!Equals(GameData.myData.SET_VOLUME, mSliderVol.value))
        {
            GameData.myData.SetVolume(mSliderVol.value);
            AudioManager.Instance.SetVolume();
        }
            
        if (GameData.myData.IS_SHOW_UI_BTN != mInteractionBtn.IsChecked)
        {
            GameData.myData.SetUIBtnOn(mInteractionBtn.IsChecked);
        }
            
        if (GameData.myData.IS_HIDE_JOYSTICK != mJoyStickBtn.IsChecked)
        {
            GameData.myData.SetJoyStickOn(mJoyStickBtn.IsChecked);
        }
            
        if (!Equals(GameData.myData.SET_CAM, mSliderCam.value))
        {
            GameData.myData.SetCamSpeed(mSliderCam.value);
        }
    }

}