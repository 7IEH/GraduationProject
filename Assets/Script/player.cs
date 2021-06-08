using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int health;
    public float speed;

    public GameObject[] Keys; // 게임 키 배열
    public bool[] hasKeys; // 가지고 있는 키

    public GameObject[] profession_num; // 클래스 선택시 사용하는 오브젝트(무기) 변수
    public Camera followCamera;
    public GameManager game;


    public int testint=1;
    public int bulletCount=0;


    float hAxis;
    float vAxis;

    float fireDelay; //공격 딜레이

    bool pro_player=false;
    bool rDown; // Run button input
    bool eDown; // Interaction button input
    bool jDown; // Jump button input
    bool isJump;
    bool isDodge;
    bool fDown; //Attack button input
    bool isFireReady; // 딜레이 완료시 공격 준비 완료
    bool isReload;
    bool isBorder; // 플레이어가 벽에 부딪힐때 통과하는 물리문제를 위한 bool 값
    bool isDamage; // 플레이어가 몬스터에게 맞고 잠시 무적이 되는 시간(연속적인 적의 공격으로 부터) 
    bool isDead;

    Vector3 moveVec;
    Vector3 dodgeMove;

    Rigidbody rigidbody;

    Animator anim;
    GameObject nearObject;
    Class_Behavior profession_player;// 플레이어가 가진 직업
    MeshRenderer[] meshs;


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
        foreach(MeshRenderer mesh in meshs)
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
        game.GameOver();
    }

    void OnTriggerStay(Collider other)
    {
        // 클래스 선택
        if (other.tag == "class")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Dungeon")// 던전 입장
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
        else if (other.tag == "Dungeon") // 던전 입장
        {
            if (hasKeys[0] == false)
            {
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
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Interaction();
        Jump();
        Dodge();
        Attack();
        Reload();
    }
    public void profession(int num)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero && !pro_player && !isDead)
        {
            pro_player = true;
            profession_player = profession_num[num].GetComponent<Class_Behavior>(); 
            if (profession_player != null) //setactive 사용시 앞에 gameobject +
            {
                //이미 정해진 직업이 있을경우
                //또는 직업에 맞지 않는 무기 장착시 사용
            }
            profession_player.gameObject.SetActive(true);

            //this.transform.position = Vector3.up * 189f;// 플레이어 선택 시 이동
        }
    }

    public void PlayerInGreenDungeon(bool enter)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            if (enter == true)
            {
                this.transform.position = new Vector3(175, 93, 69);
                
                
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
                this.transform.position = new Vector3(428.414f, 295.849f, 264.4953f);
                

            }
            else
            {
                DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                dunenter.BlueExit();
            }
        }
    }


    public void PlayerInGreenBoss()
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero)
        {
            this.transform.position = new Vector3(428.16f, 191.7046f, 264.57f);
            game.Boss();
        }
            
    }

    public void PlayerInBase()
    {
        if(!isJump && !isDodge && moveVec == Vector3.zero)
            this.transform.position = new Vector3(175.24f, 1.27f, 69.7f);
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
            else if (nearObject.tag == "Dungeon")
            {
                if (hasKeys[0] == false)
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.GreenEnter(this);
                }
                else if (hasKeys[2] == true)
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.FinalEnter(this);
                }
                else if ((hasKeys[2] == false) && (hasKeys[1] == true) && (hasKeys[0] == true))
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.RedEnter(this);
                }
                else if ((hasKeys[2] == false) && (hasKeys[1] == false) && (hasKeys[0] == true))
                {
                    DunEnter dunenter = nearObject.GetComponent<DunEnter>();
                    dunenter.BlueEnter(this);
                }
            }
            else if (nearObject.tag == "Return")
            {
                PlayerInBase();
            }
            else if (nearObject.tag == "LockKey")
            {
                game.LockKey = true;

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

        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        rDown = Input.GetButton("Run");
        eDown = Input.GetButtonDown("Interaction");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1"); // button 으로 바꿀시 마우스 누르고 있으면 계속 나감

    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeMove;

        if (isDead)
            moveVec = Vector3.zero;

        if (!isBorder)
            transform.position += moveVec * speed * (rDown ? 1f :0.3f)* Time.deltaTime;

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

    void Jump()
    {
        if (jDown && !isJump && moveVec == Vector3.zero && !isDodge && !isDead)
        {
            rigidbody.AddForce(Vector3.up * 15f, ForceMode.Impulse);
            isJump = true;
            anim.SetTrigger("doJump");
            anim.SetBool("isJump", true);
        }
    }

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
        if (pro_player == false)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = profession_player.rate < fireDelay;


        if (fDown && isFireReady && !isDodge && !isReload && !isDead)
        {
            
            profession_player.Use();
            anim.SetTrigger(profession_player.type==Class_Behavior.Type.Worrior ? "doSwing" : "doShot"); 
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
}
