using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDicEvent : MonoBehaviour
{
    [SerializeReference] private Text cardNum;
    [SerializeReference] private GameObject dialog;
    private static GameManger mgr;
    [SerializeField] CardOutLook card; 
    

    private void Awake()
    {
        if(mgr==null)mgr = GameManger.GetInstance();
    }

    public void ShowNum() 
    {
        var num = mgr.cardTypeNum[card.id];
        cardNum.text = "X" + num;
        dialog.SetActive(true);
    }

    public void CloseNum() 
    {
        dialog.SetActive(false);
    }
}
