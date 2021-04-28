using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enimies : Character
{
    //怪物行为逻辑的数组
    protected EnimiesAction[] action;
    //普通攻击内容
    [HideInInspector] public EnimiesAction normalAttack;
    //判断是否是第一回合
    protected bool firstAction = true;
    //用于随机化行为
    protected int[] actionPro;
    //回合控制器
    protected RoundControl roundControl;
    //当前战斗场景
    protected Player player;
    //当前攻击动作
    protected int currentAction = 0;
    private bool useSkill = true;

    private void OnEnable()
    {
        roundControl = RoundControl.GetInstance();
        player = BattleScene.GetInstance().player;
        OnInitialized();
        
    }

    public void SetActions(EnimiesAction[] action, int[] pro)
    {
        this.action = action;
        actionPro = pro;
    }

    /// <summary>
    /// 怪兽开始行动
    /// </summary>
    public void TakeAction()
    {
        if (action[currentAction].cost <= magic)
        {
            magic -= action[currentAction].cost;
            animator.Play(action[currentAction].animation);
        }
        else EndAction();
    }

    /// <summary>
    /// 告诉预测机制下一步行动
    /// </summary>
    /// <returns></returns>
    public EnimiesAction TellPreAction() 
    {
        return action[currentAction];
    }
        

    /// <summary>
    /// 结束行动与回合
    /// </summary>
    private void EndAction() 
    {
        if (magic >= normalAttack.cost) 
        {
            useSkill = false;
            magic -= normalAttack.cost;
            animator.Play("NormalAttack");
        }
        else
        {
            useSkill = true;
            ChooseNextAction((Random.Range(0, 101)));
            roundControl.NextRound();
        }
    }

    /// <summary>
    /// 通过随机数选择下一个行为
    /// </summary>
    /// <param name="rand"></param>
    private void ChooseNextAction(int rand)
    {
        for (int i = 0; i < actionPro.Length; i++)
        {
            if (rand < actionPro[i])
            {
                currentAction = i;
                break;
            }
        }
    }

    /// <summary>
    /// 这玩意是动画用的，我不希望别的类直接用到
    /// </summary>
    private void Calculte()
    {
        EnimiesAction act ;
        if (useSkill) act = action[currentAction];
        else act = normalAttack;
        var damage = act.damage;
        //计算伤害
        if (act.damage > 0)
        { 
             damage += buffs.courage;
            if (buffs.despair > 0) damage = (damage + 1 / 2);
        }

        if (act.drinkBlood) Health(player.Hurt(damage));
        else 
        {
            player.Hurt(damage);
            Health(act.health);
        }
              
        player.AttributeChange();
        buffs.AddArmour(act.armour);
        if (act.buff != BuffType.DESPAIR) buffs.AddBuff(act.buffLayer, act.buff);
        else player.buffs.AddBuff(act.buffLayer, act.buff);
        AttributeChange();
    }
    
    /// <summary>
    ///通知战斗管理器已经死亡 
    /// </summary>
    private void Die()
    {
        battle.EndGame(false);
    }
}
