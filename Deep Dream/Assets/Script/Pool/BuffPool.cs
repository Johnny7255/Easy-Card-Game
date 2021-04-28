using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPool : PoolData<BuffPool>
{
    [SerializeReference] private GameObject armour;
    [SerializeReference] private GameObject courage;
    [SerializeReference] private GameObject despair;

    private Dictionary<int, Queue<GameObject>> buffObjects = new Dictionary<int, Queue<GameObject>>();

    private void Awake()
    {
        instance = this;
        buffObjects.Add(1, new Queue<GameObject>());
        buffObjects.Add(2, new Queue<GameObject>());
        buffObjects.Add(3, new Queue<GameObject>());
        FillPool();
    }

    public override void FillPool()
    {
        for (int i = 0; i < 2; i++) 
        {
            var des = Instantiate(despair, transform);
            des.SetActive(false);
            var cor = Instantiate(courage, transform);
            cor.SetActive(false);
            var arm = Instantiate(armour, transform);
            arm.SetActive(false);
            buffObjects[2].Enqueue(des);
            buffObjects[1].Enqueue(cor);
            buffObjects[3].Enqueue(arm);
        }
    }

    /// <summary>
    /// 获得特定buff
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetObj(int type)
    {
        GameObject buffObject = buffObjects[type].Dequeue();
        return buffObject;
    }

    /// <summary>
    /// 将对象返回对象池
    /// </summary>
    /// <param name="type"></param>
    /// <param name="theBuff"></param>
    public void PushObj(int type,GameObject theBuff) 
    {
        theBuff.SetActive(false);
        theBuff.transform.SetParent(transform);
        buffObjects[type].Enqueue(theBuff);
    }

}
