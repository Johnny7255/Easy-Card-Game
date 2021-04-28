using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   private GameManger gmr;
   private CardPool_Player pool;
   private HashSet<GameObject> cardInHand=new HashSet<GameObject>();
   public ActionPredict predictor;

    /// <summary>
    /// 角色的初始化与回合开始抽卡
    /// </summary>
    private void Start()
    {
        isPlayer = true;
        gmr = GameManger.GetInstance();
        maxBlood = gmr.maxBlood;
        bloodInBattle = gmr.blood;
        OnInitialized();
        pool = CardPool_Player.GetInstance();
        StartCoroutine(PullCard(6));
        predictor.gameObject.SetActive(true);
    }

    /// <summary>
    /// 外界接口用的扣卡函数
    /// </summary>
    /// <param name="num"></param>
    public void TakeCard(int num) 
    {
        StartCoroutine(PullCard(num));
    }

    /// <summary>
    /// 开始回合
    /// </summary>
    public void RoundStart() 
    {
        pool.BackToWaiting();
        StartCoroutine(PullCard(5));
        predictor.gameObject.SetActive(true);
    }

    /// <summary>
    /// 回合结束
    /// </summary>
    public void EndRound() 
    {
        RemoveAllCard();
        pool.BackToWaiting();
        predictor.gameObject.SetActive(false);
    }
    

    /// <summary>
    /// 弃牌
    /// </summary>
    /// <param name="card"></param>
    public void RemoveCard(GameObject card) 
    {
        if (cardInHand.Contains(card)) cardInHand.Remove(card);
    }

    /// <summary>
    /// 用于抽牌的携程函数
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    IEnumerator PullCard(int num) 
    {
        for (int i = 0; i <num; i++) 
        {
            GameObject card = pool.GetObj();
            if (card == null) break;
            card.transform.SetParent(CardEvent.cardUI);
            card.SetActive(true);
            cardInHand.Add(card);
            yield return new WaitForFixedUpdate(); 
        }
    }

    /// <summary>
    /// 用于播放攻击动画和特效
    /// </summary>
    public void Attack()
    {
        animator.Play("Attack");
    }

    /// <summary>
    /// 一次性弃牌
    /// </summary>
    private  void RemoveAllCard()
    {
        foreach (GameObject tem in cardInHand)
        {
            tem.GetComponent<CardInstance>().CardExit();
        }
    }
}
