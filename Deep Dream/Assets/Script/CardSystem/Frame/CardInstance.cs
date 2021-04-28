using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 其会根据传入的cardFrame自动生成卡牌，或者根据ID从CardBase中获得卡牌
/// </summary>
public class CardInstance : CardOutLook
{
    [HideInInspector]public CardPool_Player cards_p;
    [HideInInspector]private CardDelegate function;

    private GameManger gameMGR;
    private Player player;
    private PrivateBuffManager buffManger;
    public int damage;
    public int magicCost;

    private void Start()
    {
        MakeCard();
        gameMGR = GameManger.GetInstance();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        buffManger = player.buffs;
    }


    /// <summary>
    /// 更新卡牌全部信息
    /// </summary>
    /// <param name="newID"></param>
    public new void RenewCard(int newID)
    {
        card = null;
        id = newID;
        MakeCard();
    }

    /// <summary>
    /// 更新卡的伤害与费用
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="cost"></param>
    public void RenewCard() 
    {
        function = new CardDelegate(card, this);
    }


    public CardDelegate GetCardFunction() 
    {
        if(damage>0) damage += buffManger.courage;
        if (buffManger.despair > 0) damage =((damage+1) / 2);
        RenewCard();
        damage = card.baseDamge;
        return function;
    }

    /// <summary>
    /// 卡牌退出动画，并且返回池子
    /// </summary>
    public void CardExit()
    {
        anitor.enabled = true;
        CardEvent.choosed = null;
        anitor.SetBool("Exit",true);
    }

    /// <summary>
    /// 播放移除卡动画
    /// </summary>
    public void PlayCardRemove() 
    {
        anitor.SetBool("Remove",true);
        RemoveCard();
    }

    /// <summary>
    /// 自动生成卡牌
    /// </summary>
    private void MakeCard()
    {
        SetCardInfo();
        function = new CardDelegate(card, this);
        damage = card.baseDamge;
        magicCost = card.baseCost;
    }
    /// <summary>
    /// 移除一次性卡牌或者移除属性卡牌
    /// </summary>
    private void RemoveCard() 
    {
        player.RemoveCard(gameObject);
        gameMGR.RemoveCard(id,0);
        Destroy(gameObject);
    }

    /// <summary>
    /// 牌返回对象池，动画调用
    /// </summary>
    private void ReturnPool() 
    {

        if (cards_p != null) 
        {
            player.RemoveCard(gameObject);
            cards_p.UsedCard(gameObject);
        }
    }


}
