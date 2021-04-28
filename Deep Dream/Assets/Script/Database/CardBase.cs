using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主卡库，会记录所有的CardFrame类型,CardBase中卡的序列会与卡ID一致
/// </summary>
[CreateAssetMenu(fileName ="New CardBase",menuName ="Cardbase/MainCardBase")]
public class CardBase :ScriptableObject
{
    public CardFrame[] allCards;
    public CardFrame[] baseCards;
    public CardFrame[] rareCards;

    /// <summary>
    /// 根据ID获取卡实例
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public CardFrame GetCard(int idx) 
    {
        return allCards[idx];
    }

    /// <summary>
    /// 返回在基础库的卡ID，根据主库完成实例化
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public int GetBaseCards(int idx) 
    {
        return baseCards[idx].CardID;
    }

    /// <summary>
    /// 返回在稀有库的卡ID，根据主库完成实例化
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public int GetRareBase(int idx) 
    {
        return rareCards[idx].CardID;
    }
}
