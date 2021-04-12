using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Behavior : MonoBehaviour
{
    public enum Type { Worrior, Gunner };
    public Type type; // 클래스 타입
    public int damage; // 데미지
    public float rate; // 공격속도
    

    public BoxCollider worriorArea; // 범위
    public TrailRenderer trailEffect; // 효과 변수
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    public void Use()
    {
        if (type == Type.Worrior)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
           
        }
        else if (type == Type.Gunner)
        {
            
            StopCoroutine("Shot");
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f) ;// 1프레임 대기
        worriorArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        worriorArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {
        //1. 총알 발사
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        //2. 탄피 배출
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10,ForceMode.Impulse);

    }

}
