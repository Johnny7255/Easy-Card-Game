using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool_Player : PoolData<CardPool_Player>
{
    private GameManger manger;
    public CardBase database;
    [SerializeReference] private GameObject cardFrame;
    private LinkedList<int> originalCards = new LinkedList<int>();
    private LinkedList<GameObject> avaliableCards = new LinkedList<GameObject>();
    private LinkedList<GameObject> usedCards = new LinkedList<GameObject>();

    private void Awake()
    {
        instance = this;  
    }

    private void Start()
    {
        manger = GameManger.GetInstance();
        //获取角色所拥有卡的内容
        foreach (int id in manger.allCards)
        {
            for (int i = 0; i < manger.cardTypeNum[id]; i++)
            {
                originalCards.AddFirst(id);
            }
        }
        FillPool();
    }


    /// <summary>
    /// 弃牌
    /// </summary>
    /// <param name="obj"></param>
    public void UsedCard(GameObject obj)
    {
        PushObj(obj,usedCards,Mathf.RoundToInt(Random.Range(0,1))>0);
    }
    
    /// <summary>
    /// 初始化卡池
    /// </summary>
    public override void FillPool()
    {
        foreach (int id in originalCards)
        {
            int rand = Mathf.RoundToInt(Random.Range(-1, 1));
            var card = Instantiate(cardFrame, transform);
            card.GetComponent<CardInstance>().RenewCard(id);
            card.GetComponent<CardInstance>().cards_p = this;
            if (rand > -1) PushObj(card, avaliableCards, true);
            else PushObj(card, usedCards, true);
            card.SetActive(false);
        }
        BackToWaiting();
    }

    /// <summary>
    /// 从池中获取卡牌
    /// </summary>
    /// <returns></returns>
    public override GameObject GetObj()
    {
        GameObject tem = null;
        if (avaliableCards.Count > 0)
        {
            tem = avaliableCards.First.Value;
            avaliableCards.RemoveFirst();
        }
        return tem;
    }

    /// <summary>
    /// 从池中推入卡牌
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="list"></param>
    /// <param name="order"></param>
    public override void PushObj(GameObject obj, LinkedList<GameObject> list, bool order)
    {
        obj.transform.SetParent(gameObject.transform);
        obj.SetActive(false);
        if (order) list.AddFirst(obj);
        else list.AddLast(obj);
    }

    /// <summary>
    /// 弃牌堆卡牌返回卡池
    /// </summary>
    public void BackToWaiting() 
    {
        int time = usedCards.Count;
        for (int i = 0; i < time; i++) 
        {
            avaliableCards.AddLast(usedCards.First.Value);
            usedCards.RemoveFirst();
        }
    }

}
