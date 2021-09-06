using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public player player;
    public GameObject EnoughGold; 
    public GameObject NoEnoughGold;
    bool inter;
    public void GeneralShop(int num)
    {
        int index = num;
        if (index == 1)
        {
            if (player.Gold >= 1000)
            { 
                NoEnoughGold.SetActive(false);
                EnoughGold.SetActive(true);
                player.HpPotion++;
                player.Gold -= 1000;
            }
            else
            {
                EnoughGold.SetActive(false);
                NoEnoughGold.SetActive(true);
            }
        }
        else if(index == 2)//2번째칸
        {

        }
    }
}
