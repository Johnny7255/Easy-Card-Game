using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvent : SingletonMono<UIEvent>
{
    public static bool mouseIn = true;
    public GameObject cardPoint;
    public GameObject arrow;


    // Start is called before the first frame update
    void Awake()
    {
        cardPoint.SetActive(false);
        arrow.SetActive(false);
        instance = this;
    }

    /// <summary>
    /// 送给撤销按钮的
    /// </summary>
    public void DeleteChange()
    {
        if (CardEvent.choosed == null) return;
        GameObject cardTem = CardEvent.choosed;
        cardTem.GetComponent<CardEvent>().goBack();
        CardEvent.choosed = null;
    }



    //下面两个方法是为了修复一个bug，避免牌跑出边界

    public void MouseIn() 
    {
        mouseIn = true;
    }

    public void MouseOut() 
    {
        mouseIn = false;
    }
}
