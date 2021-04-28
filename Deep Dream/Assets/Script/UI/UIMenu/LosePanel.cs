using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel
{
    [SerializeReference] Animator animator;
    protected override void OnClick(string btnName)
    {
        GetUp();
    }

    /// <summary>
    /// 关动画的
    /// </summary>
    private void StopAni()
    {
        animator.enabled = false;
    }
}
