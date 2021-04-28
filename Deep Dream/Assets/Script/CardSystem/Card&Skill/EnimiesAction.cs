using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物的ai攻击行为
/// </summary>
[CreateAssetMenu(fileName = "New EnAction", menuName = "EnAction/new EnAction")]
public class EnimiesAction:ScriptableObject
{
    public int cost;
    public int damage;
    public int armour;
    public int health;
    //buff层数
    public int buffLayer;
    public BuffType buff;
    //播放的动画的名字
    public string animation;
    //是否吸血
    public bool drinkBlood;
}
