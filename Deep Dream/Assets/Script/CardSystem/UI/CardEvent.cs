using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : MonoBehaviour
{
    /// <summary>
    /// 用于UI端对卡牌的各种操作
    /// </summary>
    private static  Transform gameUI;
    public static Transform cardUI;
    private static UIEvent uiEvent;
    private static Transform cardPoint;
    public bool dragable = true;
    public static GameObject choosed;

    private void Start()
    {
        uiEvent = UIEvent.GetInstance();
        gameUI =GameObject.FindGameObjectWithTag("GameUI").transform;
        cardUI = GameObject.FindGameObjectWithTag("CardUI").transform;
        cardPoint = uiEvent.cardPoint.transform;
    }

    /// <summary>
    /// 强调卡牌
    /// </summary>
    public void HighLight()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    /// <summary>
    /// 取消卡牌强调
    /// </summary>
    public void DisHighLight() 
    {
       if(!gameObject.Equals(choosed)) transform.localScale = new Vector3(1f, 1f, 1f);
    }

    /// <summary>
    /// 拉扯卡牌，当卡牌在特定位置时固定
    /// </summary>
    public void Drag()
    {
        if (Vector3.Distance(transform.position, cardPoint.transform.position) > 15&&choosed==null)
        {
            transform.SetParent(gameUI);
            if (UIEvent.mouseIn)
            {
                transform.position = Input.mousePosition;
                uiEvent.cardPoint.SetActive(true);
            }
            else 
            {
                uiEvent.cardPoint.SetActive(false);
                goBack();
                choosed = null;
            } 
        }
        else if(choosed == null)
        {
            transform.position = cardPoint.transform.position;
            choosed = gameObject;
            uiEvent.cardPoint.SetActive(false);
            uiEvent.arrow.SetActive(true);
        }
    }

    /// <summary>
    /// 释放卡牌，判断卡牌应该返回手牌区还是留在释放区域
    /// </summary>
    public void Drop()
    {
        if (Vector3.Distance(transform.position, cardPoint.transform.position) > 15&& choosed == null)
        {
            goBack();
            choosed = null;
            dragable = true;
        }
        else if(choosed==null)
        {
            transform.position = cardPoint.transform.position;
            uiEvent.cardPoint.SetActive(false);
            dragable = false;
            choosed = gameObject;
            uiEvent.arrow.SetActive(true);
        }
    }

    /// <summary>
    /// 将卡牌返回手牌区域
    /// </summary>
    public void goBack() 
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.SetParent(cardUI);
        uiEvent.cardPoint.SetActive(false);
        uiEvent.arrow.SetActive(false);
        dragable = true;
    }


}
