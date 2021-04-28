using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    BattleScene battle;

    private void Start()
    {
        battle = BattleScene.GetInstance();
    }
    void Die() 
    {
        battle.EndGame(true); 
    }
}
