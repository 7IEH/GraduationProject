using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "왔구나, 주인공...\n마왕이 또 심심해서 마을에 장난을 쳤어..\n","마왕성에 갈거라고?","역시 주인공!\n너라면 그렇게 행동할줄 알았어!","마왕에게 이번에는 따끔하게 혼좀 내고 와줘!","아 그리고 잊은게 있었는데\n철제점 아저씨가 마왕성에 가기전에 들르라고했어!"});
        talkData.Add(1001, new string[] { "화이팅!" });
        talkData.Add(1002, new string[] { "어 주인공! \n 마왕성에 갈 생각이지!", "너라면 그럴거라고 생각하고\n루나에게 말해뒀지!", "자, 검이다!", "마왕성까지 가는길은 험난하니까\n무기하나 없이 가는건 힘들거야","이번에는 다시는 장난 못치게 부탁한다구!","아 그러고보니, 사과 농장 아주머니가 널 찾더라!"});
        talkData.Add(1003, new string[] { "화이팅!" });
        talkData.Add(1004, new string[] { "아이고 주인공 왔구나\n 마왕성에 갈 생각인거 알고 있단다.", "자 마왕성까지 가는길에 먹을만한 사과 주스를 만들었단다.","든든히 먹고\n마왕의 심술을 좀 막아주렴..."});
        talkData.Add(1005, new string[] { "화이팅하려무나!" });
    }

    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

}
