using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplayer : SingletonMono<CardDisplayer>
{
    private GameObject cardFrame;
    public GameObject cardPrefab;
    public List<GameObject> cards=new List<GameObject>();
    [SerializeReference] private Transform left;
    [SerializeReference] private Transform right;
    [SerializeReference] private Transform parent;

    private void Awake()
    {

    }
    private void Start()
    {

    }
    /// <summary>
    /// 用于展示以及排列卡牌
    /// </summary>
    private void DisplayCards() 
    {
        
    }

    /// <summary>
    /// 根据卡片ID实例化卡牌
    /// </summary>
    /// <param name="cardID"></param>
    public void AddCard(int cardID) 
    {
   
    }

    public void RemoveCard() 
    {
    
    } 
}
