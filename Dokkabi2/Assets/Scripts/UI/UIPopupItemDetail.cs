using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItemDetail: UIPopup
{
    // Start is called before the first frame update
    public Button _closeButton;
    public TMP_Text _title;
    public TMP_Text _desc;
    public List<GameObject> _listItemImgs = new List<GameObject>();
    public List<GameData.ItemData> _listUserItems = new List<GameData.ItemData>();
    public Button _btnPrev;
    public Button _btnNext;
    private int _curItemIdx = 0;

    void Start()
    {
    }

    public void Init(Action del, string msg, string title = "", int itemType = 0 )
    {
        
        _listUserItems = GameData.myData.userItems;

        if (_listUserItems.Count == 0)
        {
            PopupManager.Instance.OpenPopupNotice("보유한 아이템이 없습니다.");
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
        
        
        _curItemIdx = itemType;
        if (GameData.IsEndItemIdx(_curItemIdx) || GameData.IsNoneItemIdx(_curItemIdx))
            _curItemIdx = 0;
        
        SetItemInfo(_curItemIdx);
        
        _btnNext.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            _curItemIdx++;
            if (GameData.IsEndItemIdx(_curItemIdx))
            {
                _curItemIdx--;
                return;
            }
            SetItemInfo(_curItemIdx);
        });
        
        _btnPrev.onClick.AddListener(delegate
        {
            AudioManager.Instance.PlayClick();
            _curItemIdx--;
            if (GameData.IsNoneItemIdx(_curItemIdx))
            {
                _curItemIdx++;
                return;
            }
            SetItemInfo(_curItemIdx);
        });

    }

    public void SetItemInfo(int itemType = 0)
    {
        var info = _listUserItems[itemType];
        _title.text = info._name;
        _desc.text = info._desc;
        for (int i = 0; i < _listItemImgs.Count; i++)
        {
            _listItemImgs[i].SetActive(i==itemType);
        }
    }

        
}
