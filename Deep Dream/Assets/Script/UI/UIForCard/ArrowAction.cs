using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 箭头是用于判断卡牌的作用对象
/// </summary>
public class ArrowAction : MonoBehaviour
{
    private Player player;
    private Enimies enimy;
    private TXControl TX;
    [SerializeReference]  private Image image;
    private bool chooseable=false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enimy = GameObject.FindGameObjectWithTag("Enimies").GetComponent<Enimies>();
        TX = GameObject.FindGameObjectWithTag("TX").GetComponent<TXControl>();
    }
    // Update is called once per frame
    void Update()
    {
        //箭头跟随鼠标
        transform.LookAt(Input.mousePosition);

        //选择目标
        chooseable = Mathf.Abs(transform.localRotation.x) < 0.1;

        if (Input.GetMouseButton(1))
        {
            if (chooseable) UseCard();
            else PutBackTheCard();
        }

        if (chooseable) image.color = Color.red;
        else image.color = Color.white;
    }

    /// <summary>
    /// 使用卡牌
    /// </summary>
    private void UseCard() 
    {
        player.predictor.Repredict();
        var instance = CardEvent.choosed.GetComponent<CardInstance>();
        if (Attackable(instance))
        {
            //选择卡牌作用对象
            if (transform.localRotation.y > 0)
            {
                enimy.WriteEffect(instance.GetCardFunction());
                TX.PlayTX(instance.card, false);
            }
            else
            {
                player.WriteEffect(instance.GetCardFunction());
                TX.PlayTX(instance.card, true);
            }
            if (instance.card.removeInBattle) instance.PlayCardRemove();
            else instance.CardExit();
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 判断角色是否可以攻击
    /// </summary>
    /// <returns></returns>
    private bool Attackable(CardInstance instance) 
    {
        if (player.magic >= instance.costValue)
        {
            player.magic -= instance.costValue;
            player.AttributeChange();
            player.Attack();
            return true;
        }
        PutBackTheCard();
        return false;
    }

    /// <summary>
    /// 把卡放手牌
    /// </summary>
    private void PutBackTheCard() 
    {
        CardEvent.choosed.GetComponent<CardEvent>().goBack();
        CardEvent.choosed = null;
    }

}
