using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 获得该场战斗下的玩家与怪物，并且初始化特定单例
/// </summary>
public class BattleScene : SingletonMono<BattleScene>
{
    [Header("场景人物")]
    public Player player;
    public Enimies enimy;
    public GameManger gmr;

    [Header("条件触发UI")]
    public GameObject Win;
    public GameObject Lose;
    public GameObject BuffDes;
    public Text buffText;
    public Text layerText;

    private int playerBlood;
    private int enimiesBlood;
    private void Awake()
    {
        instance = this;
        gmr = GameManger.GetInstance();
        if (gmr.level < 0) gmr.Initialize();
        playerBlood = player.blood;
        enimiesBlood = enimy.blood;
    }

    /// <summary>
    /// 时事更新角色血量的状态，用于控制场景“假事件”
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <param name="blood"></param>
    public void AttributeChange(bool isPlayer, int blood)
    {
        if (isPlayer) playerBlood = blood;
        else enimiesBlood = blood;
    }

    /// <summary>
    /// 结束游戏根据是谁扑街选择调用面板
    /// </summary>
    /// <param name="isPlayer"></param>
    public void EndGame(bool isPlayer) 
    {
        if (isPlayer) Lose.SetActive(true);
        else Win.SetActive(true);
    }
}