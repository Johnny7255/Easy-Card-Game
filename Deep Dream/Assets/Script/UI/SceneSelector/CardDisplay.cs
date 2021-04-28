using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    private GameManger mgr;
    [SerializeReference] private GameObject CardFrame;
    private HashSet<int> cardOnPanel=new HashSet<int>(); 
    private void OnEnable()
    {
        mgr = GameManger.GetInstance();
        Display();
    }

    private void Display() 
    {
        foreach (int cardID in mgr.allCards) 
        {
            if (cardOnPanel.Contains(cardID)) continue;
            GameObject card = Instantiate(CardFrame,transform);
            card.GetComponent<CardOutLook>().RenewCard(cardID);
            cardOnPanel.Add(cardID);
        }        
    }

}
