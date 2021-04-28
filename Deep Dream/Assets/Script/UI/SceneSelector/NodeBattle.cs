using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NodeBattle : EventNode
{
    [SerializeReference] private int sceneID;
    public  void EntryEvent()
    {
        function.nodeFunction += JoinBattle;
        BaseEntryEvent();
    }

    /// <summary>
    /// 进入战斗
    /// </summary>
    public void JoinBattle() 
    {
        SceneManager.LoadScene(sceneID);
    }
}
