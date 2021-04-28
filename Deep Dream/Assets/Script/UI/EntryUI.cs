using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 纯粹动画类容
/// </summary>
public class EntryUI : MonoBehaviour
{
    public Animator ani;

    private void Start()
    {
        ani.Play("entry");
    }
    void Die() 
    {
        Destroy(gameObject);
    }
}
