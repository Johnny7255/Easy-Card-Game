using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodNode : EventNode
{
    // Start is called before the first frame update
    public void EntryEvent()
    {
        function.nodeFunction += EatFood;
        BaseEntryEvent();
    }

    public void EatFood()
    {
        int blood = mgr.blood;
        if (blood + 5 > mgr.maxBlood) blood = mgr.maxBlood;
        else blood += 5;
        mgr.SetBloodValue(blood);
    }
}
