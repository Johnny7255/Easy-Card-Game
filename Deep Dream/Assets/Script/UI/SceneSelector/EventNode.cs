using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地图中所有节点的父类，同时储存特殊按钮的效果
/// </summary>
public abstract class EventNode : MonoBehaviour
{
    protected GameManger mgr;
    [HideInInspector] public Button button;
    [SerializeReference] public EventNode[] nextNodes;
    [SerializeReference] private Sprite desSprite;
    [TextArea]
    [SerializeReference] protected string destxt;
    [SerializeReference] protected string buttontxt;
    [SerializeReference] private bool isSceneNode;
    private SceneSelector selector;
    //委托，为了给每个不同节点的按钮赋予特殊效果
    protected NodeDelegate function=new NodeDelegate();

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        mgr = GameManger.GetInstance();
        selector = SceneSelector.GetInstance();
    }


    /// <summary>
    /// 进入并且开启下一个节点
    /// </summary>
    public void BaseEntryEvent() 
    {
        selector.desPanel.SetActive(true);
        int nodeID;
        selector.nodeDir.TryGetValue(this,out nodeID);
        mgr.node = nodeID;
        if (isSceneNode) 
        {
            selector.cancleButt.interactable = false;
        }
        selector.desImage.sprite = desSprite;
        selector.ResetNodes();
        selector.desText.text = destxt;
        selector.buttonText.text = buttontxt;
        foreach (EventNode idx in nextNodes) 
        {
            idx.button.interactable = true;
        }
        selector.buFunction = function;
    }
}
