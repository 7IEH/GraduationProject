using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public GameObject LockTrigger;
    public GameObject EnterLock1;
    public GameObject EnterLock2;

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
        //DunEnterInPlayer(); 던 인터 에러 테스트용
    }

    public void StageStart()
    {
        LockTrigger.SetActive(false);
        EnterLock1.SetActive(true);
        EnterLock2.SetActive(true);
        isBattle = true;
        
    }

    public void StageEnd()
    {
        EnterLock1.SetActive(false);
        EnterLock2.SetActive(false);
        isBattle = false;
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

    public void DunEnterInPlayer()
    {
        Debug.Log("진입");
        for (int MobCount=0; MobCount < 4; MobCount++)
        {
            
            GameObject instantEnemy = Instantiate(enemies[MobCount], enemyZones[MobCount].position, enemyZones[MobCount].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = player.transform;
            enemy.gamemanager = this;
            enemyCnt++;
        }
    }

    

    public void DunExitInPlayer()
    {
        player.transform.position = Vector3.up * 189f;
    }

    void LateUpdate()
    {
        playerHealthTxt.text = player.health +"/";
        if (LockKey == true)
        {
            StageEnd();
        }
    }
}
