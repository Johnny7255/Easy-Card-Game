using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDelegate
{
    /// <summary>
    /// 获取作用对象的属性
    /// </summary>
    public Character character;
    private PrivateBuffManager buffManager;

    public delegate void cardDelegate();
    public cardDelegate cardFunction;

    int damage =0;
    int heal = 0;
    int armour = 0;

    int courage = 0;
    int despair = 0;
    int cardNum =0;

    /// <summary>
    /// 根据CardFrame的内容动态调整委托
    /// </summary>
    /// <param name="card"></param>
    public  CardDelegate(CardFrame card,CardInstance cardInstance)
    {
        //获得卡属性
        cardFunction = null;
        buffManager = BattleScene.GetInstance().player.buffs;
        damage = cardInstance.damage;
        heal = card.health;
        armour = card.Armour;
        despair = card.despair;
        courage = card.courage;
        cardNum = card.num;

        cardFunction += GetCard;

        //封装委托
        if (card.type == CardType.ATTACK)
        {
            cardFunction += MakeDamage;
            cardFunction += AddArmour;
            if (card.bloodDrink) 
            {
                cardFunction -= MakeDamage;
                cardFunction += DrinkBlood;
            }
        }
        else if (card.type == CardType.HEALTH)
        {
            cardFunction += Heal;
        }
        else 
        {
            cardFunction += MakeDamage;
            cardFunction += AddCourage;
            cardFunction += AddDespair;
        }
    }
    
    //通用

    /// <summary>
    /// 造成伤害
    /// </summary>
    /// <param name="blood"></param>
    private void MakeDamage() 
    {
        character.Hurt(damage);
    }

    //Attack类型牌使用
    /// <summary>
    /// 增加护甲,攻击盾只给玩家护甲,非攻击任意
    /// </summary>
    /// <param name="armour"></param>
    private void AddArmour() 
    {
        if (damage < 1)
        {
            character.buffs.AddArmour(armour);
        }
        else 
        {
            var player = BattleScene.GetInstance().player;
            player.buffs.AddArmour(armour);
        }
    }

    /// <summary>
    /// 吸血，顺便造成伤害
    /// </summary>
    private void DrinkBlood() 
    {
        BattleScene.GetInstance().player.Health(character.Hurt(damage));
        BattleScene.GetInstance().player.AttributeChange();
    }

    //Health类型使用
    /// <summary>
    /// 恢复blood的血量
    /// </summary>
    /// <param name="blood"></param>
    private void Heal() 
    {
        character.Health(heal);
    }

    //Buff类型使用

    /// <summary>
    /// 增加眩晕层数
    /// </summary>
    /// <param name="layer"></param>
    private void AddDizz() { }

    /// <summary>
    /// 增加流血层数
    /// <param name="layer"></param>
    private void AddBleeding() 
    {
    
    }

    /// <summary>
    /// 增加绝望层数
    /// <param name="layer"></param>
    private void AddDespair() 
    {
        buffManager.AddBuff(despair,BuffType.DESPAIR);
    }

    /// <summary>
    /// 增加勇气层数
    /// </summary>
    /// <param name="layer"></param>
    private void AddCourage() 
    {
        buffManager.AddBuff(courage, BuffType.COURAGE);
    }
    /// <summary>
    /// 过牌用
    /// </summary>
    private void GetCard() 
    {
        BattleScene.GetInstance().player.TakeCard(cardNum);
    }
}
