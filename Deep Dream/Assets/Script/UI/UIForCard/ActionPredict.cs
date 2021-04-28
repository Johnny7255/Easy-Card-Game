using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///怪物行为预告
/// </summary>
public class ActionPredict : MonoBehaviour
{
   [SerializeReference] private Image attact;
   [SerializeReference] private Image buff;
   [SerializeReference] private Text damageTxt;
    private BattleScene battle;
    private Enimies eni;

    private void Awake()
    {
        battle = BattleScene.GetInstance();
        eni = battle.enimy;
        Repredict();
    }

    private void OnEnable()
    {
        Repredict();
    }

    /// <summary>
    /// 改变预测内容
    /// </summary>
    public void Repredict() 
    {
        EnimiesAction firstAct = eni.TellPreAction();
        var norAct = eni.normalAttack;
        int courage = eni.buffs.courage;

        if (courage > 0 || firstAct.buff == BuffType.COURAGE || norAct.buff == BuffType.COURAGE) attact.color = Color.white;
        else attact.color = Color.black;
        if (norAct.buff != BuffType.NULL || firstAct.buff != BuffType.NULL) buff.color = Color.magenta;
        else buff.color = Color.white;

        WriteDamage();
    }

    /// <summary>
    /// 预测伤害,暂时不考虑给自己套虚弱
    /// </summary>
    private void WriteDamage() 
    {
        var firstAct = eni.TellPreAction();

        var norAct = eni.normalAttack;
        int magic = eni.magic;
        int courage = eni.buffs.courage;
        int damage = (eni.buffs.despair > 0) ? (firstAct.damage + 1) / 2 : firstAct.damage;

        if (damage > 1) damage += courage;
        int actionTime = (magic - firstAct.cost) / norAct.cost;

        for (int i = 0; i < actionTime; i++)
        {
            if (norAct.damage > 0) damage += norAct.damage + courage;
            if (norAct.buff == BuffType.COURAGE) courage += norAct.buffLayer;
        }
        damageTxt.text = damage.ToString();
    }

}
