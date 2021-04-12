using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Select : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    player enterplayer;

    public void Enter(player player)
    {
        enterplayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Choose_Class(int index)
    {

    }
}
