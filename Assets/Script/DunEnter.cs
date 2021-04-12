using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunEnter : MonoBehaviour
{
    public RectTransform uiGroup;
    player enterplayer;
    GameManager game;

    public void Enter(player player)
    {
        enterplayer = player;
        uiGroup.anchoredPosition = Vector3.zero;

    }

    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 2000;
    }
}
