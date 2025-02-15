
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterTestScene : BaseScene
{
   
    protected override void InitScene()
    {
        base.InitScene();
        m_eSceneType = GameData.eScene.CharacterTestScene;
        SetUIManager();
        
    }
    

    public override void Clear(Action del)
    {
        //Player.instance.PlayEffect("Chara_DISAPPEAR");
        if (del != null)
        {
            StartCoroutine(ClearSceneAfter(del));
        }
        Debug.Log("UI TestScene is clear.");

    }
        
    IEnumerator ClearSceneAfter(Action del)
    {
        yield return new WaitForSeconds(0.5f);
        del();
    }


    
    public override void DelFunc()
    {

    }
}
