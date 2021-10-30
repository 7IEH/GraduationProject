using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator GeneralPanel;
    public Text generalText;
    public TalkManager talkManager;
    public Animator TutorialPanel;
    public Text TutorialText;
    public Animator LunaTalkpanel;
    public Animator Oldmanpanel;
    public Animator OldFemalepanel;
    public Text LunaText;
    public Text OldmanText;
    public Text OldfemaleText;
    public GameObject scanObject;
    public bool isChat = false;
    bool tu_next1 = false;
    bool tu_next2 = false;
    bool tu_next3 = false;
    bool tu_next4 = false;
    bool tu_next5 = false;
    bool tu_index = false;
    public objData objdata;

    int gate_num;

    public Text cointext;
    public Text timetext;
    public Text hppotiontext;

    public Image greenkey_img;
    public Image bluekey_img;
    public Image redkey_img;
    public Image blackkey_img;
    public Image whitekey1_img;
    public Image whitekey2_img;
    public Image whitekey3_img;




    GameObject LunaObject;
    public GameObject menuCam;
    public GameObject gameCam;
    public GameObject LockTrigger;
    public GameObject LockTrigger2;
    public GameObject LockTrigger3;
    public GameObject LockTrigger4;
    public GameObject LockTrigger5;
    public GameObject LockTrigger6;
    public GameObject EnterLock1;
    public GameObject EnterLock2;
    public GameObject EnterLock3;
    public GameObject EnterLock4;
    public GameObject EnterLock5;
    public GameObject EnterLock6;
    public GameObject EnterLock7;
    public GameObject EnterLock8;
    public GameObject EnterLock9;
    public GameObject EnterLock10;
    public GameObject EnterLock11;
    public GameObject EnterLock12;
    public GameObject EnterLock13;
    public GameObject returnbase;

    public GameObject greenkey;
    public GameObject bluekey;
    public GameObject redkey;

    public player player;
    public Boss boss;
   
    public int stage;
    public float playTime;
    public bool isBattle;
    public int enemyCntA;
    public int enemyCntB;
    public int enemyCntC;
    public int enemyCnt;
    public int talkIndex;

    public GameObject menuPanel;
    public GameObject GamePanel;
    public GameObject OverPanel;
    public Text playerHealthTxt;

    public Transform[] enemyZones;
    public GameObject[] enemies;
    public List<int> enemyList;

    public bool LockKey;//스테이지 잠금 해제 조건

    void Awake()
    {
        enemyList = new List<int>();
    }

    public void GameStart()
    {

        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        GamePanel.SetActive(true);
        TutorialPanel.SetBool("isShow", true);

        player.gameObject.SetActive(true);
        Tutorial(1);
        
    }

    public void GeneralStore(bool show)
    {
        if (show == true)
        {
            GeneralPanel.SetBool("isShow", show);
            generalText.text = "어서오렴!";
            
        }
        else if(show == false)
        {
            GeneralPanel.SetBool("isShow", show);
        }

    }
    public void Tutorial(int tu_num)
    {
        int index = tu_num;
        if (index == 1)
        {
            if (player.moveVec == Vector3.zero)
            {
                TutorialText.text = "방향키는 WASD 입니다.";
                tu_next1 = true;
            }
        }
        else if (index == 2)
        {
            if (player.rDown == false)
            {
                TutorialText.text = "대쉬는 방향키를 누른 상태에서 SHIFT를 눌러주세요.";
                tu_next2 = true;
            }
            
        }
        else if (index == 3)
        {
            if (player.jDown == false)
            {
                TutorialText.text = "닷지는 방향키를 누른 상태에서 SPACE를 눌러주세요.";
                tu_next3 = true;
            }
        }
        else if (index == 4)
        {
            TutorialText.text = "기본 조작법은 끝났습니다.\n마을로 이동해주세요.";
        }

        else if (index == 5)
        {
            TutorialText.text = "마을입니다.\n 루나가 당신을 찾고있습니다.\n루나를 찾아주세요!";
            
        }
        else if (index == 6)
        {
            TutorialPanel.SetBool("isShow", true);
            TutorialText.text = "철제점 아저씨가 당신을 찾고 있습니다.\n마을 오른쪽에 있는 철제점을 찾아주세요!";
        }
        else if (index == 7)
        {
            player.profession(0);
            TutorialPanel.SetBool("isShow", true);
            TutorialText.text = "농장 아주머니가 당신을 찾고 있습니다.\n철제점 오른쪽에 있는 농장 아주머니를 찾아주세요!";

        }
        else if (index == 8)
        {
            player.HpPotion = 2;
            TutorialPanel.SetBool("isShow", true);
            TutorialText.text = "여행에 필요한 모든 것을 챙겼습니다.\n마을 왼쪽 상단에 마을 입구를 찾아 여행을 시작해주세요!";
        }
        else if (index == 9)
        {
            TutorialPanel.SetBool("isShow", true);
            TutorialText.text = "공격 키는 마우스 왼쪽 버튼입니다.\n앞에 있는 몬스터를 잡아주세요!";   
        }
        else if (index == 10)
        {
            tu_next4 = true;
            if (player.health == 100)
            {
                player.health = 80;
            }
            else
            {

            }
            TutorialPanel.SetBool("isShow", true);
            TutorialText.text = "체력이 달았습니다.\n1번을 눌러서 체력을 회복해주세요!";
        }
        else if (index == 11)
        {
            tu_next5 = true;
            player.health = 50;
            TutorialPanel.SetBool("isShow", true);
            TutorialText.text = "스테이지에 떨어진 아이템에 상호작용 버튼은 e 입니다!";
        }

        
    }
    public void TalkAction(GameObject scanObj)
    {
        isChat = true;
        scanObject = scanObj;
        TutorialPanel.SetBool("isShow", false);
        
        objdata = scanObject.GetComponent<objData>();
        if (objdata.id == 1000)
        {
            LunaTalkpanel.SetBool("isShow", true);
            LunaObject = scanObject;
        }
        else if (objdata.id == 1001)
        {
            LunaTalkpanel.SetBool("isShow", true);
            LunaObject = scanObject;
        }
        else if (objdata.id == 1002)
        {
            Oldmanpanel.SetBool("isShow", true);
        }
        else if (objdata.id == 1003)
        {
            Oldmanpanel.SetBool("isShow", true);
        }
        else if (objdata.id == 1004)
        {
            OldFemalepanel.SetBool("isShow", true);
        }
        else if (objdata.id == 1005)
        {
            OldFemalepanel.SetBool("isShow", true);
        }
        Talk(objdata.id, objdata.isNPC);
        
    }

    public void GeneralStorebuy()
    {
        if (player.Gold < 1000)
        {
            generalText.text = "음; 돈이 좀 모자란거같은데?";
        }
        else
        {
            player.Gold -= 1000;
            player.HpPotion += 1;
            generalText.text = "언제나 고맙구나~";
        }
    }

    void Talk(int id,bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isChat = false;
            talkIndex = 0;
            if (id == 1000)
            {
                objdata.id++;
                LunaTalkpanel.SetBool("isShow", false);
                Tutorial(6);
                return;
            }
            else if (id == 1001)
            {
                LunaTalkpanel.SetBool("isShow", false);
                return;
            }
            else if (id == 1002)
            {
                objdata.id++;
                Oldmanpanel.SetBool("isShow", false);
                Tutorial(7);
                return;
            }
            else if (id == 1003)
            {
                Oldmanpanel.SetBool("isShow", false);
                return;
            }
            else if (id == 1004)
            {
                objdata.id++;
                OldFemalepanel.SetBool("isShow", false);
                Tutorial(8);
                return;
            }
            else if (id == 1005)
            {
                OldFemalepanel.SetBool("isShow", false);
                return;
            }
     

        }
        if (isNpc)
        {
            if (id == 1000)//luna
            {
                LunaText.text = talkData;
            }
            else if (id == 1001)
            {
                LunaText.text = talkData;
            }
            else if(id == 1002)
            {
                OldmanText.text = talkData;
            }
            else if (id == 1003)
            {
                OldmanText.text = talkData;
            }
            else if (id == 1004)
            {
                OldfemaleText.text = talkData;
            }
            else if (id == 1005)
            {
                OldfemaleText.text = talkData;
            }
        }
        else
        {
            
        }
        talkIndex++;
    }

    public void StageStart(int gate)
    {
        if (gate == 0)
        {
            gate_num = 0;
            GameObject instantEnemy = Instantiate(enemies[0], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = player.transform;
            enemy.gamemanager = this;
            enemyCnt++;
        }
        else if (gate == 1)
        {
            gate_num = 1;
            LockTrigger.SetActive(false);
            EnterLock1.SetActive(true);
            EnterLock2.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 0; MobCount < 5; MobCount++)
            {

                GameObject instantEnemy = Instantiate(enemies[0], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.gamemanager = this;
                enemyCnt++;
            }
            isBattle = true;
        }
        else if (gate == 2)
        {
            gate_num = 2;
            LockTrigger2.SetActive(false);
            EnterLock3.SetActive(true);
            EnterLock4.SetActive(true);
            EnterLock5.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 6; MobCount < 11; MobCount++)
            {

                GameObject instantEnemy = Instantiate(enemies[0], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.gamemanager = this;
                enemyCnt++;
            }
            isBattle = true;
        }
        else if (gate == 3)
        {
            gate_num = 3;
            LockTrigger3.SetActive(false);
            EnterLock6.SetActive(true);
            EnterLock7.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 11; MobCount < 17; MobCount++)
            {

                GameObject instantEnemy = Instantiate(enemies[0], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.gamemanager = this;
                enemyCnt++;
            }
            isBattle = true;
        }
        else if (gate == 4)
        {
            gate_num = 4;
            LockTrigger4.SetActive(false);
            EnterLock8.SetActive(true);
            EnterLock9.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 17; MobCount < 21; MobCount++)
            {

                GameObject instantEnemy = Instantiate(enemies[0], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.gamemanager = this;
                enemyCnt++;
            }
            isBattle = true;

        }
        else if (gate == 5)
        {
            gate_num = 5;
            LockTrigger5.SetActive(false);
            EnterLock10.SetActive(true);
            EnterLock11.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 21; MobCount < 26; MobCount++)
            {

                GameObject instantEnemy = Instantiate(enemies[0], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.gamemanager = this;
                enemyCnt++;
            }
            isBattle = true;
        }
        else if (gate == 6)
        {
            gate_num = 6;
            LockTrigger6.SetActive(false);
            EnterLock12.SetActive(true);
            EnterLock13.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 26; MobCount < 30; MobCount++)
            {

                GameObject instantEnemy = Instantiate(enemies[0], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.target = player.transform;
                enemy.gamemanager = this;
                enemyCnt++;
            }
            isBattle = true;
        }
    }

    public void Boss()
    {
        gate_num = 7;
        isBattle = true;
        GameObject instantEnemy = Instantiate(enemies[3], enemyZones[5].position, enemyZones[5].rotation);
        Enemy enemy = instantEnemy.GetComponent<Enemy>();
        enemy.target = player.transform;
        enemy.gamemanager = this;
        enemyCnt++;
        
    }

    public void StageEnd()
    {
        if (gate_num == 0)
        {
            TutorialPanel.SetBool("isShow", false);
            isBattle = false;
        }
        else if (gate_num == 1)
        {
            EnterLock1.SetActive(false);
            EnterLock2.SetActive(false);
            isBattle = false;
        }
        else if (gate_num == 2)
        {
            EnterLock3.SetActive(false);
            EnterLock4.SetActive(false);
            EnterLock5.SetActive(false);
            isBattle = false;
        }
        else if (gate_num == 3)
        {
            EnterLock6.SetActive(false);
            EnterLock7.SetActive(false);
            isBattle = false;
        }
        else if (gate_num == 4)
        {
            EnterLock8.SetActive(false);
            EnterLock9.SetActive(false);
            isBattle = false;

        }
        else if (gate_num == 5)
        {
            EnterLock10.SetActive(false);
            EnterLock11.SetActive(false);
            isBattle = false;
        }
        else if (gate_num == 6)
        {
            EnterLock12.SetActive(false);
            EnterLock13.SetActive(false);
            isBattle = false;
        }
        else if (gate_num == 7)
        {
            greenkey.SetActive(true);
            returnbase.SetActive(true);
            isBattle = false;
        }
    }


    public void GameOver()
    {
        GamePanel.SetActive(false);
        OverPanel.SetActive(true);
    }

    public void Gamequit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }



    void Update()
    {
        playTime += Time.deltaTime;
    }

    public void DunExitInPlayer()
    {
        player.transform.position = Vector3.up * 189f;
    }

    void LateUpdate()
    {
        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int second = (int)(playTime % 60);

        hppotiontext.text = "X " + player.HpPotion.ToString();
        timetext.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
        cointext.text = player.Gold.ToString();
        playerHealthTxt.text = player.health +"/";
        if (enemyCnt<=0 && isBattle==true)
        {
            StageEnd();
        }
        if (player.moveVec != Vector3.zero && tu_next1 == true)
        {
            tu_next1 = false;
            Tutorial(2);
        }
        if (player.rDown==true && tu_next2 == true)
        {
            tu_next2 = false;
            Tutorial(3);
        }
        if (player.jDown == true && tu_next3 == true && player.moveVec != Vector3.zero)
        {
            tu_next3 = false;
            Tutorial(4);
        }
        if (player.oneDown == true && tu_next4 == true)
        {  
            tu_next4 = false;
            TutorialPanel.SetBool("isShow", false);
        }
        if (player.eDown == true && tu_next5 == true)
        {
            tu_next5 = false;
            TutorialPanel.SetBool("isShow", false);
        }

        if (player.hasKeys[0] == true)
        {
            whitekey1_img.gameObject.SetActive(false);
            greenkey_img.gameObject.SetActive(true);
        }
        else if (player.hasKeys[1] == true)
        {
            whitekey2_img.gameObject.SetActive(false);
            bluekey_img.gameObject.SetActive(true);
        }
        else if (player.hasKeys[2] == true)
        {
            whitekey3_img.gameObject.SetActive(false);
            redkey_img.gameObject.SetActive(true);
        }
    }
}
