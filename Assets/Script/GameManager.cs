using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int gate_num;

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

        player.gameObject.SetActive(true);
        
    }

    public void StageStart(int gate)
    {
        if (gate == 1)
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
        if (gate_num == 1)
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

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

  

    

    public void DunExitInPlayer()
    {
        player.transform.position = Vector3.up * 189f;
    }

    void LateUpdate()
    {
        playerHealthTxt.text = player.health +"/";
        if (enemyCnt<=0 && isBattle==true)
        {
            StageEnd();
        }
    }
}
