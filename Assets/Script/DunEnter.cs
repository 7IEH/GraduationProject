using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DunEnter : MonoBehaviour
{
    public Animator TutorialPanel;
    public RectTransform uiGroup1,uiGroup2,uiGroup3,uiGroup4;
    player enterplayer;
    GameManager game;

    public void GreenEnter()
    {
        TutorialPanel.SetBool("isShow", false);
        uiGroup1.anchoredPosition = Vector3.zero;

    }

    public void GreenExit()
    {
        uiGroup1.anchoredPosition = Vector3.down * 2000;
    }

    public void BlueEnter()
    {
        
        uiGroup2.anchoredPosition = Vector3.zero;

    }

    public void BlueExit()
    {
        uiGroup2.anchoredPosition = Vector3.down * 3000;
    }
    public void RedEnter()
    {
        
        uiGroup3.anchoredPosition = Vector3.zero;

    }

    public void RedExit()
    {
        uiGroup3.anchoredPosition = Vector3.down * 4000;
    }

    public void FinalEnter()
    {
        
        uiGroup4.anchoredPosition = Vector3.zero;

    }

    public void FinalExit()
    {
        uiGroup4.anchoredPosition = Vector3.down * 5000;
    }

}
