using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffIcon : MonoBehaviour
{
   [TextArea]
   [SerializeReference] private string describe;
    public int layer;
    private BattleScene battle;


    private void Awake()
    {
        battle = BattleScene.GetInstance();
    }

    public void SetLayer(int layer) 
    {
        this.layer = layer;
    }

    public void PEnter() 
    {
        battle.BuffDes.SetActive(true);
        battle.layerText.text = "X"+layer;
        battle.buffText.text = describe;
    }

    public void PExit() 
    {
        battle.BuffDes.SetActive(false);
    }
}
