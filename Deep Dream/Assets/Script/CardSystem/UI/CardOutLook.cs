using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 负责改变卡牌的外观
/// </summary>
public class CardOutLook : MonoBehaviour
{
    public CardBase cardBase;
    public int id = 0;
    public CardFrame card;
    [SerializeReference] protected Image icon;
    [SerializeReference] protected Text describe;
    [SerializeReference] protected Text cardName;
    [SerializeReference] protected Text cost;
    [SerializeReference] protected Animator anitor;
    [HideInInspector] public int costValue;
    // Start is called before the first frame update
  
    private void Start()
    {
        SetCardInfo();
    }

    protected virtual void OnEnable() 
    {
        anitor.SetBool("Exit", false);
        anitor.Play("cardEnter");
    }
    /// <summary>
    /// 设置卡牌UI信息
    /// </summary>
    protected void SetCardInfo() 
    {
        if (card == null) card = cardBase.GetCard(id);
        else id = card.CardID;
        icon.sprite = card.image;
        describe.text = card.describe;
        cardName.text = card.cardName;
        cost.text = card.baseCost.ToString();
        costValue = card.baseCost;
    }
    /// <summary>
    /// 结束动画播放
    /// </summary>
    protected void StopAni()
    {
        anitor.enabled = false;
    }

    /// <summary>
    /// 更新卡牌外观
    /// </summary>
    /// <param name="newID"></param>
    public void RenewCard(int newID)
    {
        card = null;
        id = newID;
        SetCardInfo();
    }

}
