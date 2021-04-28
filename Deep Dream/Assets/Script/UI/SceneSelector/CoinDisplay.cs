using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于显示金币的数值
/// </summary>
public class CoinDisplay : MonoBehaviour
{
    private GameManger mgr;
    [SerializeReference]private Text gtext;
    private int coin; 

    // Start is called before the first frame update
    void Start()
    {
        mgr = GameManger.GetInstance();
        coin = mgr.gold;
        gtext.text = coin.ToString();
        mgr.StatusChange += ChangeGoldText;
    }

    /// <summary>
    /// 改变文字描述
    /// </summary>
    private void ChangeGoldText() 
    {
        coin = mgr.gold;
        gtext.text = coin.ToString();
    }

    private void OnDestroy()
    {
        mgr.StatusChange -= ChangeGoldText;
    }
}
