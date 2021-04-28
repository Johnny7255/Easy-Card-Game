using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buff
/// </summary>
public enum BuffType : int
{
    DIZZ = 5, BLEEDING = 4, ARMOUR = 3, DESPAIR = 2, COURAGE = 1, NULL = 0
}

/// <summary>
/// 私人Buff管理器，挂载在角色身上，辅助Character
/// </summary>
public class PrivateBuffManager : MonoBehaviour
{
    [HideInInspector]
    public int courage;
    [HideInInspector]
    public int despair;

    /// <summary>
    /// ps 护甲在buff管理器中只是因为它有buff的所有特点，但它在该游戏理解上并不是buff
    /// </summary>
    [HideInInspector]
    public int armour;

    /// <summary>
    /// Buff放置面板
    /// </summary>
    [SerializeReference] private Transform panel;

    /// <summary>
    /// 储存BuffObject的数组
    /// </summary>
    private GameObject[] buffArray = new GameObject[5];

    /// <summary>
    /// 存buff对象的对象池
    /// </summary>
    private BuffPool pool;


    private void Start()
    {
       pool=BuffPool.GetInstance();
    }

    /// <summary>
    ///按照回合的消除buff 
    /// </summary>
    public void DecreaseBuff()
    {
        RemoveCourage();
        RemoveDespair();
    }

    /// <summary>
    ///清除所有buff 
    /// </summary>
    public void ClearBuff()
    {
        armour = 0;
        courage = 0;
        despair = 0;
        for (int i = 1; i < 4; i++) PushIcon(i);
    }

    /// <summary>
    /// 添加特定buff
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    public void AddBuff(int layer, BuffType type)
    {
        if (layer < 1) return;

      if (type == BuffType.COURAGE)
        {
            if (courage == 0) PutBuffIcon(1);
            courage += layer;
        }
        else if (type == BuffType.DESPAIR)
        {
            if (despair == 0) PutBuffIcon(2);
            despair += layer;
        }

        int idx = 0;
        switch (type)
        {
            case BuffType.COURAGE: idx = 1; break;
            case BuffType.DESPAIR: idx = 2; break;
        }


        if (idx == 1) buffArray[idx].GetComponent<BuffIcon>().SetLayer(courage);
        else if (idx == 2) buffArray[idx].GetComponent<BuffIcon>().SetLayer(despair);
    
    }

    /// <summary>
    /// 加护甲
    /// </summary>
    /// <param name="layer"></param>
    public void AddArmour(int layer) 
    {
        if (armour ==0&&layer>0) PutBuffIcon(3);
        armour += layer;
       if(layer>0) buffArray[3].GetComponent<BuffIcon>().SetLayer(armour);
    }

    /// <summary>
    /// 减护甲
    /// </summary>
    /// <param name="layer"></param>
    public void RemoveArmour(int layer)
    {
        if (armour > 0)
        {
            if (layer >= armour)
            {
                armour = 0;
                PushIcon(3);
            }
            else 
            {
                armour -= layer;
                buffArray[3].GetComponent<BuffIcon>().SetLayer(armour);
            }           
        }
    }

    /// <summary>
    /// 放置BUFF图标
    /// </summary>
    /// <param name="type"></param>
    private void PutBuffIcon(int type)
    {
        buffArray[type] = pool.GetObj(type);
        buffArray[type].transform.position = transform.position;
        buffArray[type].transform.SetParent(panel);
        buffArray[type].transform.localScale = new Vector3(1, 1, 1);
        buffArray[type].SetActive(true);
    }

    /// <summary>
    /// 放回BUFF图标
    /// </summary>
    private void PushIcon(int type) 
    {
        pool.PushObj(type, buffArray[type]);
        buffArray[type] = null;
    }

    //下面内容都是各个buff的移除方法，因为不同buff移除方式不一样，所以没有一起写

    private void RemoveCourage()
    {
        if (courage > 0) 
        {
            courage--;
            if (courage == 0) PushIcon(1);
            else buffArray[1].GetComponent<BuffIcon>().SetLayer(courage);
        }
    }

    private void RemoveDespair()
    {
        if (despair > 0) 
        {
            despair -= ((despair + 1) / 2);
            if (despair == 0) PushIcon(2);
            else buffArray[2].GetComponent<BuffIcon>().SetLayer(despair);
        }
    }
}
