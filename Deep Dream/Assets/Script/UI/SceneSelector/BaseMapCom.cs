using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 记录该地图按钮的基础事件
/// </summary>
public class BaseMapCom : MonoBehaviour
{
    public GameObject dicPanel;
    public Slider bloodStream;
    public Slider magicStream;
    public Text blood;
    public Text magic;
    private GameManger mgr;

    private void Awake()
    {
        mgr = GameManger.GetInstance();
        mgr.StatusChange += SetStatusPanel;
    }

    private void Start()
    {
        SetStatusPanel();
    }

    /// <summary>
    /// 设置基础面板
    /// </summary>
    public void SetStatusPanel() 
    {
        bloodStream.maxValue = mgr.maxBlood;
        bloodStream.value = mgr.blood;
        magicStream.maxValue = mgr.maxMagic;
        magicStream.value = mgr.maxMagic;
        blood.text = mgr.blood.ToString();
        magic.text = mgr.maxMagic.ToString();

    }

    /// <summary>
    /// 打开图鉴
    /// </summary>
    public void OpenDicPanel() 
    {
        dicPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭图鉴
    /// </summary>
    public void CloseDicPanel() 
    {
        dicPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        mgr.StatusChange -= SetStatusPanel;
    }
}
