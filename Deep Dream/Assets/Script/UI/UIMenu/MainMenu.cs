using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : BasePanel
{
    protected override void OnClick(string btnName)
    {
        if (btnName.Equals("Night")) StartGame();
        else Application.Quit();
    }

    /// <summary>
    /// 开始游戏，初始化单例
    /// </summary>
    private void StartGame() 
    {
        GameManger.GetInstance().Initialize();
        SceneManager.LoadScene(1);
    }

}
