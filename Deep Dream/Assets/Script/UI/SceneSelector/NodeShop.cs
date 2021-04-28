using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeShop : EventNode
{
    [SerializeReference]private GameObject shopPanel;

    public  void EntryEvent() 
    {
        function.nodeFunction += OpenShop;
        BaseEntryEvent();
    }
    /// <summary>
    /// 开店
    /// </summary>
    private void OpenShop() 
    {
        shopPanel.SetActive(true);
    }
}
