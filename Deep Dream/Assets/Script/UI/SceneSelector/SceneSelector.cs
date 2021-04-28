using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneSelector : SingletonMono<SceneSelector>
{
    private GameManger mgr;
    /// <summary>
    /// 描述面板
    /// </summary>
    public GameObject desPanel;
    public Text desText;
    public Text buttonText;
    public Button cancleButt;
    public Button entryButt;
    public Image desImage;
    //储存所有节点
    public EventNode[] mapNodes;
    public Dictionary<EventNode, int> nodeDir=new Dictionary<EventNode, int>();

    public NodeDelegate buFunction;

    private void Awake()
    {
        instance = this;
        mgr=GameManger.GetInstance();
        if (mgr.level == -1) mgr.Initialize();
        desPanel.SetActive(false);
        //将地图放入字典，好保存下一个节点
        for (int i=0;i<mapNodes.Length;i++)
        {
            print(nodeDir.ContainsKey(mapNodes[i]));
            if (!nodeDir.ContainsKey(mapNodes[i]))  nodeDir.Add(mapNodes[i],i);
        }
    }

    private void Start()
    {
        ResetNodes();
        if (mgr.node == -1) mapNodes[0].button.interactable = true;
        else 
        {
            foreach (EventNode node in mapNodes[mgr.node].nextNodes) 
            {
                node.button.interactable = true;
            }
        }
    }

    /// <summary>
    /// 重置所有节点按钮
    /// </summary>
    public void ResetNodes() 
    {
        foreach (EventNode node in mapNodes) node.button.interactable = false;
    }
    /// <summary>
    /// 取消的按钮
    /// </summary>
    public void Cancle() 
    {
        desPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 非取消按钮的功能
    /// </summary>
    public void EntryNode() 
    {
        buFunction.nodeFunction();
    }
    
}

public class NodeDelegate 
{
    public delegate void nodeDelegate();
    public  nodeDelegate nodeFunction;
}