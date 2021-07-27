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
        talkData.Add(1000, new string[] { "안녕, 주인공 내 이름은 루나야 반가워!","우선 옆에 있는 성석과 상호작용하여 직업을 고른뒤 다시 와줘!" });
        talkData.Add(1001, new string[] { "아직 직업을 고르지 않았구나 \n옆에 성석과 상호작용하여 직업을 먼저 고르고 나에게 다시 말을 걸어줘!" });
        talkData.Add(1002, new string[] { "너에게 정말 어울리는 직업이야", "이제 부터 무기를 사용하는 방법을 알려줄게", "마우스 왼쪽 버튼을 누르면 공격할 수 있어!" });
        talkData.Add(1003, new string[] { "이제 준비가 다 된거같아", "화살표를 따라 마을 외곽에 있는 던전의 입구로 가서\n 3개의 던전을 클리어하여 3개의 열쇠를 모아와줘!" });
    }

    public string GetTalk(int id,int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

}
