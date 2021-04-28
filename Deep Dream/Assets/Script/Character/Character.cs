using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 包含角色的基础属性以及必要组件
/// </summary>
public class Character : MonoBehaviour
{
    [Header("基础属性")]
    public int maxBlood;
    public int maxMagic;
    /// <summary>
    /// 场景中血量，用于剧情
    /// </summary>
    public int bloodInBattle;

    public PrivateBuffManager buffs;

    [HideInInspector]public int blood;
    [HideInInspector]public int magic;
    
    [Header("组件")]
    [SerializeReference]protected Slider bloodSlider;
    [SerializeReference]protected Slider magicSlider;
    [SerializeReference]protected Text bloodValue;
    [SerializeReference]protected Text magicValue;
    [SerializeReference]protected Animator animator;
    protected BattleScene battle;
    protected bool isPlayer=false;
    /// <summary>
    /// 卡牌影响，接受委托类型
    /// </summary>
     private CardDelegate effect=null;
    
    /// <summary>
    /// 初始化属性
    /// </summary>
    protected void OnInitialized()
    {
        magicSlider.maxValue = maxMagic;
        magicSlider.minValue = 0;
        bloodSlider.maxValue = maxBlood;
        bloodSlider.minValue = 0;
        magic = maxMagic;
        blood = bloodInBattle;
        magicSlider.value = magic;
        bloodSlider.value = blood;
        bloodValue.text = blood.ToString();
        magicValue.text = magic.ToString();
        battle = BattleScene.GetInstance();
    }

    /// <summary>
    /// 随时更改角色的属性
    /// </summary>
    public void AttributeChange() 
    {
        if (bloodSlider.value > blood) animator.Play("Hurt");
        bloodSlider.value = blood;
        bloodValue.text = blood.ToString();
        magicSlider.value = magic;
        magicValue.text = magic.ToString();
        battle.AttributeChange(isPlayer, blood);
        if (blood < 1) animator.Play("Die");
    }

    /// <summary>
    /// 获取具体的委托内容
    /// </summary>
    /// <param name="effect"></param>
    public void WriteEffect(CardDelegate effect) 
    {
        this.effect = effect;
    }

    /// <summary>
    /// 收到伤害的代码
    /// </summary>
    /// <param name="damage"></param>
    public int Hurt(int damage) 
    {
        if (damage <= buffs.armour) 
        {
            buffs.RemoveArmour(damage);
        }
        else
        {
            damage -= buffs.armour;
            buffs.RemoveArmour(buffs.armour);
            blood -= damage;
        }
        if (blood < 1) blood = 0;
        return damage;
    }

    /// <summary>
    /// 治疗用代码
    /// </summary>
    /// <param name="health"></param>
    public void Health(int health) 
    {
        blood += health;
        if (blood > maxBlood) blood = maxBlood;
    }

    /// <summary>
    /// 执行委托实现具体效果
    /// </summary>
    public void TakeEffect()
    {
        if (effect == null) return;
        effect.character = this;
        effect.cardFunction();
        effect = null;
        AttributeChange();
    }

    /// <summary>
    /// 回合开始结算Buff
    /// </summary>
    public void BuffCountHead() 
    {
        buffs.RemoveArmour(buffs.armour);
        AttributeChange();
    }

    /// <summary>
    /// 回合结束结束buff
    /// </summary>
    public void BuffCountEnd() 
    {
        buffs.DecreaseBuff();
        magic = maxMagic;
        AttributeChange();
    }

    /// <summary>
    /// 告诉战斗场景管理器死亡事实
    /// </summary>
    private void Die() 
    {
        battle.EndGame(isPlayer);
    }
}
