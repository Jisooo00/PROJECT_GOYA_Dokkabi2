
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameTestScene_1 : BaseScene
{
    protected override void InitScene()
    {
        base.InitScene();
        m_eSceneType = GameData.eScene.GameTestScene_1;
        
    }
    

    public override void Clear(Action del)
    {
        //Player.instance.PlayEffect("Chara_DISAPPEAR");
        if (del != null)
        {
            StartCoroutine(ClearSceneAfter(del));
        }
        Debug.Log("GameTestScene is clear.");

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
