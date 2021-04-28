using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 当角色胜利，调用面板
/// </summary>
public class WinningPanel : BasePanel
{
    [SerializeReference] Animator animator;
    private BattleScene battle;
    private GameManger mgr;
    [SerializeReference] private int gold;
    [SerializeReference] private GameObject[] reward = new GameObject[3];

    protected void Start()
    {
        battle = BattleScene.GetInstance();
        mgr = GameManger.GetInstance();
    }
    protected override void OnClick(string btname)
    {
        if (btname.Equals("ESC")) GetUp();
        else GoToNext();
    }


    /// <summary>
    /// 前往下一关
    /// </summary>
    public void GoToNext()
    {
        mgr.SetBloodValue(battle.player.blood);
        mgr.ChangeGoldValue(gold);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 关动画的
    /// </summary>
    private void StopAni()
    {
        animator.enabled = false;
    }

    /// <summary>
    /// 动画用于显示卡
    /// </summary>
    private void ShowCards()
    {
        foreach (GameObject card in reward) card.SetActive(true);
    }
}
