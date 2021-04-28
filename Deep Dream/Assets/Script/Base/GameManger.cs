using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 贯穿全局的单例，与角色成长有关
/// </summary>
public class GameManger : SingletonMono<GameManger>
{
    public int maxBlood;
    public int maxMagic;
    public int blood;
    public int gold;
    public int exp;
    public int level=-1;
    public int node=0;//战斗节点
    /// <summary>
    /// 记录拥有卡牌的种类
    /// </summary>
    public HashSet<int> allCards=new HashSet<int>();
    /// <summary>
    /// 记录卡库中不同卡牌的数量
    /// </summary>
    public Dictionary<int, int> cardTypeNum = new Dictionary<int, int>();
    /// <summary>
    /// 事件，在特定情况下更新角色的数值
    /// </summary>
    public delegate void StatusValueHandle();
    public event StatusValueHandle StatusChange;

    public void Awake()
    {
        if (instance != null) 
        {
            Destroy(gameObject);
        }
        else instance = this;
        DontDestroyOnLoad(this);
        
    }
    /// <summary>
    /// 刚刚进入游戏的话就初始化属性与卡牌
    /// </summary>
    public void Initialize() 
    {
        maxBlood = 30;
        maxMagic = 5;
        blood = 30;
        gold = 0;
        exp = 0;
        level = 1;

        AddNewCard(0,4);
        AddNewCard(1,4);
        AddNewCard(2,1);
        AddNewCard(3,1);

        node = -1;
    }

    /// <summary>
    /// 全局添加新卡
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="num"></param>
    public void AddNewCard(int cardID, int num) 
    {
        if (allCards.Contains(cardID)) cardTypeNum[cardID] += num;
        else 
        {
            cardTypeNum.Add(cardID, num);
            allCards.Add(cardID);
        }
    }

    /// <summary>
    /// 全局删卡
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="num"></param>
    public void RemoveCard(int cardID, int num) 
    {
        if (allCards.Contains(cardID)) 
        {
            cardTypeNum[cardID] -= num;
            if (cardTypeNum[cardID] < 1) allCards.Remove(cardID);
        }
    }
    /// <summary>
    /// 用于修改金币量
    /// </summary>
    /// <param name="gold"></param>
    public void ChangeGoldValue(int gold) 
    {
        if (this.gold + gold < 0) this.gold = 0;
        else this.gold += gold;
        if (StatusChange != null) StatusChange();
    }
    /// <summary>
    /// 同步血量
    /// </summary>
    /// <param name="blood"></param>
    public void SetBloodValue(int blood) 
    {
        if (blood != this.blood) 
        {
            this.blood = blood;
            if (StatusChange != null) StatusChange();
        }
    }
    /// <summary>
    /// 同步蓝上限
    /// </summary>
    /// <param name="blood"></param>
    public void SetMagicValue(int magic)
    {
        if (magic != maxMagic)
        {
            maxMagic = magic;
            StatusChange();
        }
    }
}
