using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 回合控制器，让结束回合按钮与让怪物ai自己跳过回合
/// 并且结算相应buff
/// </summary>
public class RoundControl : SingletonMono<RoundControl>
{
    [HideInInspector] public bool playerRound=true;
    [SerializeReference] private Button cancle;
    [SerializeReference] private Button nextRound;
    [SerializeReference] private GameObject arr;
    private BattleScene scene;
    private Player player;
    private Enimies enimies;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        scene = BattleScene.GetInstance();
        player = scene.player;
        enimies = scene.enimy; 
    }

    /// <summary>
    /// 切换回合
    /// </summary>
    public void NextRound() 
    {
        scene.BuffDes.SetActive(false);
        arr.SetActive(false);
        playerRound=!playerRound;
        if (playerRound) ToPlayerRound();
        else ToEnimiesRound();
    }

    /// <summary>
    /// 轮到玩家回合
    /// </summary>
    private void ToPlayerRound() 
    {
        player.magic = player.maxMagic;
        player.BuffCountHead();
        enimies.BuffCountEnd();
        player.RoundStart();
        StartCoroutine(EnableButtons());
    }

    /// <summary>
    /// 轮到怪物回合
    /// </summary>
    private void ToEnimiesRound() 
    {
        player.magic = player.maxMagic;
        enimies.BuffCountHead();
        player.BuffCountEnd();
        player.EndRound();
        cancle.interactable = false;
        nextRound.interactable = false;
        enimies.TakeAction();
    }

    /// <summary>
    /// 为了防止乱操作，延迟开启按钮
    /// </summary>
    /// <returns></returns>
    private IEnumerator EnableButtons() 
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 2) 
            {
                cancle.interactable = true;
                nextRound.interactable = true;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
