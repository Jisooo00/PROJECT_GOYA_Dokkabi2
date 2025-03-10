using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
    [SerializeField] private TMP_Text mTextScript;
    [SerializeField] private Sprite[] m_listSprites;
    public UISpeaker m_speakDokkabi;
    public UISpeaker m_speakNPC;
    public float m_fScaleDokkabi = 1f;
    public float m_fScaleNP0001 = 1f;
    public float m_fScaleNP0002 = 1f;
    public float m_fScaleNP0003 = 1f;
    
    [Serializable]
    public class UISpeaker
    {
        public GameObject m_goj;
        public TMP_Text mTextName;
        public Image mImgPortrait;
        public Image mImgNameBg;
        private bool isSpeaking;
        public void SetSpeaking(bool speak)
        {
            isSpeaking = speak;
            if (!isSpeaking)
            {
                mImgPortrait.color = Color.gray;
                mImgNameBg.color = Color.gray;
            }
            else
            {
                mImgPortrait.color = Color.white;
                mImgNameBg.color = Color.white;
            }
        }

        public void SetName(string name)
        {
            if (name == "dokkabi")
                name = GameData.myData.user_name;
            if (name == "np_0001")
                name = "주막주인"; //TODO 로컬 적용
            if (name == "np_0002")
                name = "산예"; //TODO 로컬 적용
            if (name == "np_0003")
                name = "여우"; 
            mTextName.text = name;
        }

        public void SetImg(Sprite img, float fScale)
        {
            var rect = mImgPortrait.GetComponent<RectTransform>();
            mImgPortrait.sprite = img;
            mImgPortrait.SetNativeSize();
            rect.localScale = Vector2.one * fScale;

        }
    }

    private int mIndex = 0;

    private Action mDelDialogAfter;
    private GameData.DialogData mData;
    private List<GameData.ScriptData> m_listScript;
    private Dictionary<string, Sprite> mDicPortraits;

    private MonsterBase npc;
    
    public void Init(GameData.DialogData data,Action del,MonsterBase _npc = null)
    {
        npc = _npc;
//        Debug.Log("InitDialogSystem");
        m_listScript = GameData.GetScript(data.m_strDialogID);
        mIndex = 0;
        if (m_listScript.Count== 0)
        {
            GameManager.DialogAction(data);
        }
        else
        {
            mData = data;
            mDelDialogAfter = del + delegate
            {
                if (!data.m_bReplay)
                {
                    //PlayerPrefs.SetString(string.Format("{0}_{1}", GameData.myData.user_name, data.m_strDialogID), "true");
                    //PlayerPrefs.Save();
                    GameData.SetDialogPlayed(data.mObjectID,data.m_strDialogID);
                }
                if(npc != null)
                    npc.SetQuestionMark();
                GameManager.DialogAction(data);
            };
            
            mDicPortraits = new Dictionary<string, Sprite>();
            SetSpritePortraits();
            InitSpeakerUI();
            SetDialog();
        }
    }

    void InitSpeakerUI()
    {
        m_speakDokkabi.m_goj.SetActive(false);
        m_speakNPC.m_goj.SetActive(false);
    }

    
    public void OnClick()
    {/*
        mIndex++;
        if (mIndex >= m_listScript.Count)
        {
            mDelDialogAfter();
        }
        else
        {
            SetDialog();
        }*/
    }

    public void SetDialog()
    {
        var script = m_listScript[mIndex].m_strScript;
        if (script.Contains("#NICK_NAME"))
            script = script.Replace("#NICK_NAME", GameData.myData.user_name);
        mTextScript.text = script;
        string img = string.Format("img_portrait_{0}",m_listScript[mIndex].GetPortraitImgName());
        if (m_listScript[mIndex].IsDokkabi)
        {
            if(!m_speakDokkabi.m_goj.activeSelf)
                m_speakDokkabi.m_goj.SetActive(true);
            m_speakDokkabi.SetSpeaking(true);
            if (m_speakNPC.m_goj.activeSelf)
                m_speakNPC.SetSpeaking(false);
            m_speakDokkabi.SetName(m_listScript[mIndex].m_strSpeaker);
            if (mDicPortraits.ContainsKey(img))
            {
                m_speakDokkabi.SetImg(mDicPortraits[img],m_fScaleDokkabi);
            }
            
        }
        if (m_listScript[mIndex].IsNPC)
        {
            if(!m_speakNPC.m_goj.activeSelf)
                m_speakNPC.m_goj.SetActive(true);
            m_speakNPC.SetSpeaking(true);
            if (m_speakDokkabi.m_goj.activeSelf)
                m_speakDokkabi.SetSpeaking(false);
            m_speakNPC.SetName(m_listScript[mIndex].m_strSpeaker);
            float fScale = 1f;
            if (mDicPortraits.ContainsKey(img))
            {

                if (m_listScript[mIndex].m_strSpeaker == "np_0001")
                    fScale = m_fScaleNP0001; 
                else if (m_listScript[mIndex].m_strSpeaker == "np_0002")
                    fScale = m_fScaleNP0002; 
                else if (m_listScript[mIndex].m_strSpeaker == "np_0003")
                    fScale = m_fScaleNP0003; 
                m_speakNPC.SetImg(mDicPortraits[img],fScale);
            }
        }

    }
    
    private void SetSpritePortraits()
    {
        foreach (var sprite in m_listSprites)
        {
            if(mDicPortraits.ContainsKey(sprite.name))
                continue;
            
            mDicPortraits.Add(sprite.name,sprite);
        }
    }

}
