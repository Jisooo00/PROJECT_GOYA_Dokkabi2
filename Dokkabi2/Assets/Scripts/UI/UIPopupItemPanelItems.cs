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
    private List<ItemData> _tmpItems = new List<ItemData>();

    [Serializable]
    public class ItemUI
    {
        private ItemData _data;
        public Button _btn;
        public TMP_Text _txtCnt;
        public GameObject _obj;

        public void Init(ItemData data)
        {
            _data = data;
            _txtCnt.text = _data._count.ToString();
            _btn.onClick.AddListener(delegate{});
        }

    }
    public class ItemData
    {
        public int _count;
        public string _name;
        public string _desc;
        public ItemType _type;
        
        public enum ItemType
        {
            Cloth = 0,
            Glue = 1,
        }
        public ItemData(ItemType type, string name, string desc, int count)
        {
            _type = type;
            _count = count;
            _name = name;
            _desc = desc;
        }
        
    }
    public override void Init()
    {
        var item1 = new ItemData(ItemData.ItemType.Cloth,"무명", "청포전에서 구매한 무명. 베틀로 짠 천으로 탈의 뒤를 덧대는데 필요한 재료이다.", 1);
        var item2 = new ItemData(ItemData.ItemType.Glue,"아교", "점성이 있는 물체로 주로 접착제의 용도로 쓰인다.", 2);
        
        _tmpItems.Add(item1);
        _tmpItems.Add(item2);
        SetItems();
        
    }

    public void SetItems()
    {
        for (int i = 0; i < _tmpItems.Count; i++)
        {
            var item = _tmpItems[i];
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