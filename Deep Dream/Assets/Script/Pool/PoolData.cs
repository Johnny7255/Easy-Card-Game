using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///   池子中的一列容器
/// </summary>
public abstract class PoolData<T> : SingletonMono<T> where T : SingletonMono<T>
{
    /// <summary>
    /// 初始化池子
    /// </summary>
    public abstract void FillPool();

    /// <summary>
    /// 往抽屉里面 压东西，同时决定要不要按照顺序
    /// </summary>
    /// <param name="obj"></param>
    public virtual void PushObj(GameObject obj, LinkedList<GameObject> list, bool order) { }

    /// <summary>
    /// 从抽屉里面 取东西
    /// </summary>
    /// <returns></returns>
    public virtual GameObject GetObj() 
    {
        return null;
    }
}