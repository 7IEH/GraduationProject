using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Luna : MonoBehaviour
{
    public player player;
    public GameManager manager;
    public Text Lunatext;
    public Animator LunaChat;
    public bool isChat;
    string PlayerName; // PlayerName
    int chatNum = 0;

    public void visit()
    {
        manager.isChat = true;
        manager.TutorialPanel.SetBool("isShow", false);
        Invoke("ChatShow", 0.2f);
    }

    private void ChatShow()// 나중에 변수로 player이름 추가              
    {
      
    }

}
