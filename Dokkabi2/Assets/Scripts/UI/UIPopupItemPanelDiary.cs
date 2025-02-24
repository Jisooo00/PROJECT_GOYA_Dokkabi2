using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItemPanelDiary : UIPopupItemPanel
{
    public List<GameObject> _slotsList = new List<GameObject>();
    public List<DiaryUI> _diaryButtons = new List<DiaryUI>();

    [Serializable]
    public class DiaryUI
    {
        private GameData.DiaryData _data;
        public Button _btn;
        public GameObject _obj;

        public void Init(GameData.DiaryData data)
        {
            _data = data;
            _btn.onClick.AddListener(delegate
            {
                AudioManager.Instance.PlayClick();
                PopupManager.Instance.OpenPopupItemDiary(null,(int)_data._index);
            });
        }
        
    }
    public override void Init()
    {
        SetDiary();
        
    }

    public void SetDiary()
    {
        var Diary = GameData.myData.userDiary;
        for (int i = 0; i < Diary.Count; i++)
        {
            var diary = Diary[i];
            int idx = (int)diary._index;
            if (i >= _slotsList.Count || idx >= _diaryButtons.Count) break;
            var btn = _diaryButtons[idx];
            btn.Init(diary);
		
            var tm = btn._obj.GetComponent<RectTransform>();
            tm.SetParent(_slotsList[i].transform, false);
            tm.anchoredPosition = Vector2.zero;
            btn._obj.SetActive(true);

        }
    }
    
}