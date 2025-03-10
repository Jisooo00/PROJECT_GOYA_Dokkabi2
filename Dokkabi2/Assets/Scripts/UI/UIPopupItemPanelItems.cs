using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItemPanelItems : UIPopupItemPanel
{
    public List<GameObject> _slotsList = new List<GameObject>();
    public List<ItemUI> _itemButtons = new List<ItemUI>();

    [Serializable]
    public class ItemUI
    {
        private GameData.ItemData _data;
        public Button _btn;
        public TMP_Text _txtCnt;
        public GameObject _obj;

        public void Init(GameData.ItemData data)
        {
            _data = data;
            _txtCnt.text = _data._count.ToString();
            _btn.onClick.AddListener(delegate
            {
                AudioManager.Instance.PlayClick();
                PopupManager.Instance.OpenPopupItemDetail(null,(int)_data._type);
            });
        }
        
    }
    public override void Init()
    {
        SetItems();
        
    }

    public void SetItems()
    {
        var Items = GameData.myData.userItems;
        for (int i = 0; i < Items.Count; i++)
        {
            var item = Items[i];
            int idx = (int)item._type;
            if (i >= _slotsList.Count || idx >= _itemButtons.Count) break;
            var btn = _itemButtons[idx];
            btn.Init(item);
		
            var tm = btn._obj.GetComponent<RectTransform>();
            tm.SetParent(_slotsList[i].transform, false);
            tm.anchoredPosition = Vector2.zero;
            btn._obj.SetActive(true);

        }
    }
    
}