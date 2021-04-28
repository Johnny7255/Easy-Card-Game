using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡的基础属性 
/// </summary>
public enum CardType
{
    BUFF,ATTACK,HEALTH
}

/// <summary>
/// 不同的CardInstance的ID必须有不同，包含关于卡牌的一切信息，CardInstance会根据此自动生成卡牌
/// </summary>
[CreateAssetMenu(fileName = "Card", menuName = "Card/New Card")]
public class CardFrame : ScriptableObject
{
    public string cardName;

    /// <summary>
    /// 主牌库ID
    /// </summary>
    public int CardID;
    /// <summary>
    /// 常规牌库ID（从5开始）
    /// </summary>
    public int baseID;
    /// <summary>
    /// 稀有牌库ID（从3开始）
    /// </summary>
    public int rareID;

    [TextArea]
    public string describe;
    public int baseCost;
    public int baseDamge;
    //回蓝
    public int addMagic;
    public CardType type;


    [Header("移除类型")]
    public bool removeInBattle;
    public bool removeInGame;
    public Sprite image;

    [Header("过牌")]
    public int num;

    [Header("ATTACK")]
    public int Armour;
    /// <summary>
    /// 潘墩是否吸血
    /// </summary>
    public bool bloodDrink;
    /// <summary>
    /// 烧蓝
    /// </summary>
    public int magicStole;

    [Header("BUFF")]
    public int bleeding;
    public int dizz;
    public int despair;
    public int courage;

    [Header("HEALTH")]
    public int health;

   
}

