using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物的行为控制器，所有怪物特殊行为的控制器模板
/// </summary>
public  class EniActionControl : MonoBehaviour
{
    [SerializeReference] private Enimies eni;
    [SerializeReference] private EnimiesAction[] action;
    [SerializeReference] private int[] actionPro;
    public EnimiesAction normalAttack;

    //初始化行为逻辑
    private void Start()
    {
        InitializeController();
        eni.SetActions(action, actionPro);
    }
    /// <summary>
    /// 初始化怪物行为控制器
    /// </summary>
    protected void InitializeController() 
    {
        if (action.Length != actionPro.Length) throw new System.Exception("UnmatchLogicList");
        CheckIfLogicListMatch();
        eni.normalAttack = normalAttack;
    }

    /// <summary>
    /// 检查是否存在行为逻辑错误
    /// </summary>
    protected void CheckIfLogicListMatch() 
    {
        if (action.Length != actionPro.Length||action.Length==0) throw new LogicUnmatchException();
        if (actionPro[actionPro.Length - 1] != 100) throw new LogicUnmatchException();
        var min = actionPro[0];
        foreach(int i in actionPro) 
        {
            if(min>i) throw new LogicUnmatchException();
            min = i;
        }
    }

    /// <summary>
    /// 异常，行为存在逻辑错误
    /// </summary>
    class LogicUnmatchException : System.Exception 
    {
        public LogicUnmatchException()
        { 
            print(StackTrace);
        }
    } 
}

