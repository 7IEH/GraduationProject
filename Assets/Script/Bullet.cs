using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee; // 근접 공격 범위 파괴 방지
    public bool isRock; // 보스 패턴시 파괴 방지

    void OnCollisionEnter(Collision collision)
    {
        if (!isRock&&collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

  

