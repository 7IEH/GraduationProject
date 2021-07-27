using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 네비게이션 AI 사용시 도착 함수

public class Enemy : MonoBehaviour
{
    public enum Type {A,B,C,D};
    public Type enemyType;

    public int maxHealth;
    public int curHealth;
    public Transform target; // AI 사용 인스턴트
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public BoxCollider meleeArea; // 근접 몬스터 범위
    public GameObject bullet;

    public Vector3 responPosition;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public MeshRenderer[] meshs;
    public NavMeshAgent nav; // AI 사용 인스턴트
    public Animator anim;
    public GameManager gamemanager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        responPosition = this.transform.position;

        
    }

   

    void Update()
    {
        
        if (Vector3.Distance(target.position, transform.position) > 5f&& nav.enabled&&enemyType!=Type.D)
        {
            isChase = false;
            nav.isStopped = false;
            anim.SetBool("isWalk", false);
            returnBase();
        }
        else if(Vector3.Distance(target.position, transform.position) <= 5f && nav.enabled && enemyType != Type.D)
        {
            isChase = true;
            anim.SetBool("isWalk", true);
            nav.SetDestination(target.position); //setDestionation():도착할 목표 위치 지정 함수
            nav.isStopped = false;  
        }
    }

    void returnBase()
    {
        nav.isStopped = false;
        anim.SetBool("isWalk", true);
        if (enemyType == Type.A)
        {
            nav.SetDestination(responPosition);
            if (Vector3.Distance(transform.position, responPosition) < 0.5f)
            {
                nav.isStopped = true;
                anim.SetBool("isWalk", false);
            }
        }
        else if (enemyType == Type.B)
        {
            nav.SetDestination(gamemanager.enemyZones[1].position);
            Debug.Log(Vector3.Distance(transform.position, gamemanager.enemyZones[1].position));
            if (Vector3.Distance(transform.position, gamemanager.enemyZones[1].position) < 2f)
            {
                Debug.Log("B");
                nav.isStopped = true;
                anim.SetBool("isWalk", false); //대쉬 공격 수정 사항 1. 애초에 따라오는 속도가 너무 빠름
            }
        }
        else
        {
            nav.SetDestination(gamemanager.enemyZones[2].position);
            if (Vector3.Distance(transform.position, gamemanager.enemyZones[2].position) < 0.4f)
            {
                Debug.Log("C");
                nav.isStopped = true;
                anim.SetBool("isWalk", false); // 원거리 공격 수정 사항 1. 공격 딜레이 때문에 따라오지 않음 => 일정 거리 안에서 멈춰서 사격하는걸로 바꾸기
            }
        }
        

    }

    void FixedUpdate()
    {
        FreezeVelocity();
        Targeting();
    }

    void Targeting()
    {
        if (!isDead && enemyType != Type.D)
        {
            float targetRadius = 0;
            float targetRange = 0;// 몬스터가 다가오는 거리

            switch (enemyType)
            {
                case Type.A:
                    targetRadius = 1.5f;
                    targetRange = 2.5f;
                    break;
                case Type.B:
                    targetRadius = 1f;
                    targetRange = 12f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;
                    targetRange = 25f;
                    break;
            }

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f); // 몬스터 공격 애니메이션 딜레이
                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;
                yield return new WaitForSeconds(1f);
                break;

            case Type.B:
                yield return new WaitForSeconds(0.1f); // 몬스터 공격 애니메이션 딜레이
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;
                yield return new WaitForSeconds(2f);
                break;

            case Type.C:
                yield return new WaitForSeconds(0.5f); // 몬스터 공격 애니메이션 딜레이
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;
                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;// 플레이어와의 충돌 후 velocity 값이 - 되는걸 방지
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Worrior")
        {
            Class_Behavior class_behavior = other.GetComponent<Class_Behavior>();
            curHealth -= class_behavior.damage;
            Vector3 reactVec = transform.position - other.transform.position; // 적 몬스터 사후 이동

            StartCoroutine(onDamage(reactVec));
            
        }
        else if (other.tag == "Bullet")
        {
            Bullet bul = other.GetComponent<Bullet>();
            curHealth -= bul.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);

            StartCoroutine(onDamage(reactVec));
        }
    }

    IEnumerator onDamage(Vector3 reactVec)
    {
        foreach(MeshRenderer mesh in meshs)
            mesh.material.color = Color.red;
      
        if (curHealth > 0)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.white;
        }
        else if(curHealth<=0&&!isDead)
        {
            
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.gray;

            gameObject.layer = 14;
            isDead = true;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            gamemanager.enemyCnt--;

            //사후 이동 로직
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
            //
            if (enemyType != Type.D)
                Destroy(gameObject, 4);


        }
    }
}
