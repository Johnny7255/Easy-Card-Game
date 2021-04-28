using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCard : MonoBehaviour
{
    [SerializeReference] private CardBase database;
    [SerializeReference] private CardOutLook card;
    [SerializeReference] private WinningPanel parentPanel;
    /// <summary>
    /// 卡价格
    /// </summary>
    public int cost;
    /// <summary>
    /// 是否是随机稀有度的卡
    /// </summary>
    public bool random;
    /// <summary>
    /// 是否是基础牌
    /// </summary>
    public bool baseCard;
    /// <summary>
    /// 是否是稀有牌
    /// </summary>
    public bool rareCard;
    public int ID;

    private void OnEnable()
    {
        if (random) GetInRandom();
        else if (baseCard) GetInBase();
        else if (rareCard) GetInRare();
        card.RenewCard(ID);
    }

    /// <summary>
    /// 如果是随机卡，那就抽卡
    /// </summary>
    private void GetInRandom()
    {
        int ran = (Random.Range(0, 101));
        if (ran > 20) GetInBase();
        else GetInRare();
    }
    /// <summary>
    /// 获取普通卡
    /// </summary>
    private void GetInBase() 
    {
        int normalID = (Random.Range(0, 5));
        ID = database.baseCards[normalID].CardID;
        print(ID+"Base ID"+normalID);
    }
    /// <summary>
    /// 获取稀有牌
    /// </summary>
    private void GetInRare() 
    {
        int rareID = (Random.Range(0, 3));
        ID = database.rareCards[rareID].CardID;
        print(ID+"rare ID"+rareID);
    }
    /// <summary>
    /// 选择卡牌
    /// </summary>
    public void SelectCard() 
    {
        var mgr = GameManger.GetInstance();
        if (mgr.gold >= cost) 
        {
            GameManger.GetInstance().AddNewCard(ID, 1);
            GameManger.GetInstance().ChangeGoldValue(cost * -1);
            gameObject.SetActive(false);
        }
        if (parentPanel != null)
        {
            parentPanel.GoToNext();
        }
    }

}
