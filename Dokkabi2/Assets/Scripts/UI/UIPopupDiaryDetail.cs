using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupDiaryDetail: UIPopup
{
    // Start is called before the first frame update
    public Button _closeButton;
    public TMP_Text _title;
    public TMP_Text _desc;
    public List<GameObject> _listDiaryImgs = new List<GameObject>();
    public List<GameData.DiaryData> _listUserDiary = new List<GameData.DiaryData>();
    public Button _btnPrev;
    public Button _btnNext;
    private int _curDiaryIdx = 0;

    void Start()
    {
    }

    public void Init(Action del, string msg, string title = "", int itemType = 0 )
    {
        
        _listUserDiary = GameData.myData.userDiary;

        if (_listUserDiary.Count == 0)
        {
            PopupManager.Instance.OpenPopupNotice("No Diary.");
            OnClose();
            return;
        }

        base.Init(del);
        
        _closeButton.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            m_delClose();
            this.gameObject.SetActive(false);

        });
        
        
        _curDiaryIdx = itemType;
        if (GameData.IsEndItemIdx(_curDiaryIdx) || GameData.IsNoneItemIdx(_curDiaryIdx))
            _curDiaryIdx = 0;
        
        SetItemInfo(_curDiaryIdx);
        
        _btnNext.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            _curDiaryIdx++;
            if (GameData.IsEndDiaryIdx(_curDiaryIdx))
            {
                _curDiaryIdx--;
                return;
            }
            SetItemInfo(_curDiaryIdx);
        });
        
        _btnPrev.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            _curDiaryIdx--;
            if (GameData.IsNoneDiaryIdx(_curDiaryIdx))
            {
                _curDiaryIdx++;
                return;
            }
            SetItemInfo(_curDiaryIdx);
        });

    }

    public void SetItemInfo(int itemType = 0)
    {
        var info = _listUserDiary[itemType];
        _title.text = info._title;
        _desc.text = info._desc;
        for (int i = 0; i < _listDiaryImgs.Count; i++)
        {
            _listDiaryImgs[i].SetActive(i==itemType);
        }
    }

        
}
