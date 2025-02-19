using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupItem : UIPopup
{
    // Start is called before the first frame update
    public List<UIPopupItemPanel> _panels;
    public Button _closeButton;
    private UIPopupItemPanelSetting _setting;

    void Start()
    {
    }

    public void Init(Action del, string msg, string title = "",int idx = 0)
    {
        foreach (var panel in _panels)
        {
            panel.Init();
            if (panel.GetComponent<UIPopupItemPanelSetting>() != null)
                _setting = panel.GetComponent<UIPopupItemPanelSetting>();
        }
        
        if(_setting !=null)
            del = delegate { _setting.OnSet(); }+del;
        
        base.Init(del);
        SetPanel(idx);
        _closeButton.onClick.AddListener(delegate
        {
            m_delClose();
            this.gameObject.SetActive(false);

        });
    }

    public void SetPanel(int idx)
    {
        for (int i = 0; i < _panels.Count; i++)
        {
            _panels[i].gameObject.SetActive(i == idx);
        }
    }

    public void OnButtonIdx0()
    {
        SetPanel(0);
    }

    public void OnButtonIdx1()
    {
        SetPanel(1);
    }
    
    public void OnButtonIdx2()
    {
        SetPanel(2);
    }
    
    public void OnButtonIdx3()
    {
        SetPanel(3);
    }
    
    public void OnButtonIdx4()
    {
        SetPanel(4);
    }
}