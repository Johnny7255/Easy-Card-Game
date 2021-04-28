using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TXControl : MonoBehaviour
{
    private BattleScene scene;
    [SerializeReference] private Animator leftTX;
    [SerializeReference] private Animator rightTX;
    private Player player;
    private Enimies enimy;

    void Start()
    {
        scene = BattleScene.GetInstance();
        player = scene.player;
        enimy = scene.enimy;
    }

    public void PlayTX(CardFrame card, bool isLeft)
    {
        Animator tem;
        if (isLeft) tem = leftTX;
        else tem = rightTX;

        if (card.baseDamge<1&&card.Armour > 0)
        {
            tem.Play("Shell");
        }
        else if (card.type == CardType.HEALTH) tem.Play("Heal");
        else tem.Play("Boom");
    }

    /// <summary>
    /// 动画操作的代码，结算
    /// </summary>
    public void PutOnEffect()
    {
        player.TakeEffect();
        enimy.TakeEffect();
    }


}
