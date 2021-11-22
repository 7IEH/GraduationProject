using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int HpPotion=0;
    public int Gold = 10000;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    public int health;
    public float speed;

    public GameObject[] Keys; // 게임 키 배열
    public bool[] hasKeys; // 가지고 있는 키

    public GameObject[] profession_num; // 클래스 선택시 사용하는 오브젝트(무기) 변수
    public Camera followCamera;
    public GameManager manager;

    public int testint = 1;
    public int bulletCount = 0;


    float hAxis;
    float vAxis;

    float fireDelay; //공격 딜레이

    public bool pro_player = false;
    public bool rDown; // Run button input
    public bool eDown; // Interaction button input
    public bool jDown; // Jump button input
    public bool oneDown; // 1 button input
    public bool zDown; // swap input
    bool isJump;
    bool isDodge;
    public bool fDown; //Attack button input
    bool isFireReady; // 딜레이 완료시 공격 준비 완료
    bool isReload;
    bool isBorder; // 플레이어가 벽에 부딪힐때 통과하는 물리문제를 위한 bool 값
    bool isDamage; // 플레이어가 몬스터에게 맞고 잠시 무적이 되는 시간(연속적인 적의 공격으로 부터) 
    bool isDead;

    public Vector3 moveVec;
    Vector3 dodgeMove;

    Rigidbody rigidbody;

    Animator anim;
    GameObject nearObject;
    GameObject LunaObject;
    Class_Behavior profession_player;// 플레이어가 가진 직업
    MeshRenderer[] meshs;
    public int equip=-1;

    void FreezeRotation()
    {
        rigidbody.angularVelocity = Vector3.zero;//플레이어가 탄피에 의해 자동 회전하는걸 방지
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
    }

    void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
            anim.SetBool("isJump", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Bullet enemybullet = other.GetComponent<Bullet>();
                health -= enemybullet.damage;

                bool isBossAtk = other.name == "Boss Melee Area";// 보스 넉백 공격 시 플레이어 이동 // 박스 콜라이더로 인해 밀리는 걸 방지
                StartCoroutine(OnDamage(isBossAtk));
            }

            if (other.GetComponent<Rigidbody>() != null)
                Destroy(other.gameObject);
        }
    }

    IEnumerator OnDamage(bool isBossAtk)
    {
        isDamage = true;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }

        if (isBossAtk)
            rigidbody.AddForce(transform.forward * -25, ForceMode.Impulse);
        yield return new WaitForSeconds(1f); // 무적 시간

        isDamage = false;
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }

        if (isBossAtk)
            rigidbody.velocity = Vector3.zero;

        if (health <= 0 && !isDead)
            OnDie();

    }

    void OnDie()
    {
        anim.SetTrigger("doDie");
        isDead = true;
        manager.GameOver();
    }

    void OnTriggerStay(Collider other)
    {
        // 클래스 선택
        if (other.tag == "class")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Dunenter")// 던전 입장
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Return")//집으로 귀환
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "LockKey")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Boss1")
        {
            nearObject = other.gameObject;//보스룸 입장
        }
        else if (other.tag == "GreenKey")
        {
            nearObject = other.gameObject;//보스 죽이고 얻는 키
        }
        else if (other.tag == "BlueKey")
        {
            nearObject = other.gameObject;//보스 죽이고 얻는 키
        }
        else if (other.tag == "RedKey")
        {
            nearObject = other.gameObject;//보스 죽이고 얻는 키
        }
        else if (other.tag == "Luna")
        {
            LunaObject = other.gameObject;//루나 채팅용
            nearObject = other.gameObject;//루나
        }
        else if (other.tag == "Oldman")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Oldfemale")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Generalstore")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Heart")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 클래스 선택
        if (other.tag == "class")
        {
            Class_Select class_select = nearObject.GetComponent<Class_Select>();
            class_select.Exit();
            nearObject = null;
        }
        else if (other.tag == "Dunenter") // 던전 입구에서 떠나는거
        {
           
            if (hasKeys[0] == false)
            {
                Debug.Log(nearObject);
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.GreenExit();
                nearObject = null;
            }
            else if (hasKeys[2] == true)
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.FinalExit();
                nearObject = null;
            }
            else if ((hasKeys[2] == false) && (hasKeys[1] == true) && (hasKeys[0] == true))
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.RedExit();
                nearObject = null;
            }
            else if ((hasKeys[2] == false) && (hasKeys[1] == false) && (hasKeys[0] == true))
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.BlueExit();
                nearObject = null;
            }

        }
        else if (other.tag == "Return")
        {
            nearObject = null;
        }
        else if (other.tag == "Return")
        {
            nearObject = null;
        }
        else if (other.tag == "Boss1")
        {
            nearObject = null;
        }
        else if (other.tag == "GreenKey")
        {
            nearObject = null;
        }
        else if (other.tag == "BlueKey")
        {
            nearObject = null;
        }
        else if (other.tag == "RedKey")
        {
            nearObject = null;
        }
        else if (other.tag == "Luna")
        {
            nearObject = null;
        }
        else if (other.tag == "Oldman")
        {
            nearObject = null;
        }
        else if (other.tag == "Oldfemale")
        {
            nearObject = null;
        }
        else if (other.tag == "Generalstore")
        {
            manager.GeneralStore(false);
            nearObject = null;
        }
        else if (other.tag == "Heart")
        {
            nearObject = null;
        }
        else if (other.tag == "Weapon")
        {
            nearObject = null; 
        }
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        equip = 0;
        profession(0);
        hasWeapons[1] = true;
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Interaction();
        Dodge();
        Attack();
        Reload();
        HpHeal();
        Swap();
    }

    public void Swap()
    {
        if (zDown)
        {
            Debug.Log("들어옴");
            if (hasWeapons[0] == false && hasWeapons[1] == false)
            {

            }
            else if (hasWeapons[0] == true && hasWeapons[1] == false)
            {
            }
            else if (hasWeapons[0] == true && hasWeapons[1] == true)
            {
               
                if (equip == 0)
                {
                    
                    profession(1);
                    
                }
                else if (equip == 1)
                {
                    profession(0);
                    
                }
            }
        }
    }
    public void profession(int num)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero && !isDead)
        {
            
            if (profession_player != null) //setactive 사용시 앞에 gameobject +
            {
                
                profession_player.gameObject.SetActive(false);
                profession_player = null;
                if (num == 1)
                {
                    equip = 1;
                }
                else if (num == 0)
                {
                    equip = 0;
                }
            }
            pro_player = true;
            profession_player = profession_num[num].GetComponent<Class_Behavior>();
            profession_player.gameObject.SetActive(true);
            
            nearObject = null;
        }
    }

    public void PlayerInGreenDungeon(bool enter)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            if (enter == true)
            {
                this.transform.position = new Vector3(172, 1, -72.97f);
                DunEnter dunEnter = nearObject.GetComponent<DunEnter>();
                dunEnter.GreenExit();
            }
            else
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.GreenExit();
            }
        }
    }

    public void PlayerInBlueDungeon(bool enter)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            if (enter == true)
            {
                this.transform.position = new Vector3(313.18f, 2.2f, -180.98f);
                DunEnter dunEnter = nearObject.GetComponent<DunEnter>();
                dunEnter.BlueExit();
                manager.BlueStart();
            }
            else
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.BlueExit();
            }
        }
    }

    public void PlayerInRedDungeon(bool enter)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            if (enter == true)
            {
                this.transform.position = new Vector3(315.97f, 1f, -343.47f);//블루던전 위치 바꾸기
                manager.Final_Boss();


            }
            else
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.RedExit();
            }
        }
    }

    public void PlayerInWhiteDungeon(bool enter)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            if (enter == true)
            {
                this.transform.position = new Vector3(675.3f, 1f, -412.06f);//블루던전 위치 바꾸기


            }
            else
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.FinalExit();
            }
        }
    }


    public void PlayerInGreenBoss()
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            this.transform.position = new Vector3(169.66f, 191.7046f, 264.57f);
            manager.Boss();
        }

    }

    public void PlayerInBase()
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
            this.transform.position = new Vector3(112.67f, 8.81f, 210.21f);
    }


    void Interaction()
    {
        if (eDown && nearObject != null && !isDodge && !isJump && !isDead)
        {
            if (nearObject.tag == "class")
            {
                Class_Select class_select = nearObject.GetComponent<Class_Select>();
                class_select.Enter(this);
            }
            else if (nearObject.tag == "Luna")
            {
                manager.TalkAction(nearObject);
            }
            else if (nearObject.tag == "Oldman")
            {
                manager.TalkAction(nearObject);
            }
            else if (nearObject.tag == "Oldfemale")
            {
                manager.TalkAction(nearObject);
            }
            else if (nearObject.tag == "Dunenter")
            {
                if (hasKeys[0] == false)
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.GreenEnter();
                }
                else if (hasKeys[2] == true)
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.FinalEnter();
                }
                else if ((hasKeys[2] == false) && (hasKeys[1] == true) && (hasKeys[0] == true))
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.RedEnter();
                }
                else if ((hasKeys[2] == false) && (hasKeys[1] == false) && (hasKeys[0] == true))
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.BlueEnter();
                }
            }
            else if (nearObject.tag == "Return")
            {
                PlayerInBase();
            }
            else if (nearObject.tag == "LockKey")
            {
                manager.LockKey = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "Boss1")
            {
                PlayerInGreenBoss();
            }
            else if (nearObject.tag == "GreenKey")
            {
                Item item = nearObject.GetComponent<Item>();
                int keyIndex = item.value;
                hasKeys[keyIndex] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "BlueKey")
            {
                Item item = nearObject.GetComponent<Item>();
                int keyIndex = item.value;
                hasKeys[keyIndex] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "RedKey")
            {
                Item item = nearObject.GetComponent<Item>();
                int keyIndex = item.value;
                hasKeys[keyIndex] = true;

                Destroy(nearObject);
            }
            else if (nearObject.tag == "Generalstore")
            {
                manager.GeneralStore(true);
            }
            else if (nearObject.tag == "Heart")
            {
                health += 50;
                if (health > 100)
                {
                    health = 100;
                }
                Destroy(nearObject);
            }
            else if (nearObject.tag == "Weapon")
            {

                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
            }

        }
    }

    void GetInput()
    {

        hAxis = manager.isChat ? 0 : Input.GetAxisRaw("Horizontal");
        vAxis = manager.isChat ? 0 : Input.GetAxisRaw("Vertical");
        rDown = manager.isChat ? false : Input.GetButton("Run");
        eDown = Input.GetButtonDown("Interaction");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1"); // button 으로 바꿀시 마우스 누르고 있으면 계속 나감
        oneDown = Input.GetButton("Num1");
        zDown = Input.GetButtonDown("Swap");
    }

    void HpHeal()
    {
        if (oneDown && health!=100 && !isDead && HpPotion!=0)
        {
            health += 20;
            HpPotion--;
            if (health > 100)
            {
                health = 100;
            }
        }
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeMove;

        if (isDead)
            moveVec = Vector3.zero;

        if (!isBorder)
            transform.position += moveVec * speed * (rDown ? 1f : 0.3f) * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        anim.SetBool("isRun", rDown);
    }

    void Turn()
    {
        //1. 키보드에 의한 회전
        transform.LookAt(transform.position + moveVec);

        //2. 마우스에 의한 회전
        if (fDown && !isDead)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                transform.LookAt(transform.position + nextVec);
            }
        }
    }

    /*void Jump()
    {
        if (jDown && !isJump && moveVec == Vector3.zero && !isDodge && !isDead)
        {
            rigidbody.AddForce(Vector3.up * 15f, ForceMode.Impulse);
            isJump = true;
            anim.SetTrigger("doJump");
            anim.SetBool("isJump", true);
        }
    }*/

    void Dodge()
    {
        if (jDown && !isJump && moveVec != Vector3.zero && !isDodge && !isDead)
        {
            dodgeMove = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            Invoke("Dodgeout", 0.4f);
        }
    }

    void Dodgeout()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    void Attack()
    {
        if (equip == -1)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = profession_player.rate < fireDelay;


        if (fDown && isFireReady && !isDodge && !isReload && !isDead)
        {
            Debug.Log("들어옴4");
            profession_player.Use();
            anim.SetTrigger(profession_player.type == Class_Behavior.Type.Worrior ? "doSwing" : "doShot");
            fireDelay = 0;
            bulletCount++;
            if (bulletCount == 15)
            {
                isReload = true;
            }
        }

    }

    void Reload()
    {

        if (bulletCount != 15)
            return;

        if (profession_player == null)
        {
            isReload = false;
            return;
        }


        if (profession_player.type == Class_Behavior.Type.Worrior)
        {
            isReload = false;
            return;
        }

        if (!isJump && !isDodge && !isDead)
        {
            anim.SetTrigger("doReload");
        }


        Invoke("ReloadOut", 1.5f);
    }

    void ReloadOut()
    {
        bulletCount = 0;
        isReload = false;
    }
    void LateUpdate()
    {
    }

}
