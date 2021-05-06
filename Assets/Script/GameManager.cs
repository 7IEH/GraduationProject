using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
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
    public Text playerHealthTxt;

    public Transform[] enemyZones;
    public GameObject[] enemies;
    public List<int> enemyList;

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
    }
}
