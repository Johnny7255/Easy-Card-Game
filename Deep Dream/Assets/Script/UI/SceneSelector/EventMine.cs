using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMine : EventNode
{
    public void EntryEvent() 
    {
        function.nodeFunction += EarnMoney;
        BaseEntryEvent();
    }

    public void EarnMoney() 
    {
        int val = Random.Range(0,100);
        if (val < 20) mgr.ChangeGoldValue(300);
        else if (val < 40) mgr.ChangeGoldValue(200);
        else mgr.ChangeGoldValue(100);
    }
}
