# 유기태 졸업프로젝트 portfolio
## Summary
Unity engine 기반 quarterview 로그라이크형식 Game Development project


## Teammates

![image](https://user-images.githubusercontent.com/80614927/143538186-31987473-0b1b-408c-b994-3bfdb74c859d.png)

<br/>

## Development's Tools

|Kinds         |Using |
|-------------|--------------|
|**`Language`**   |C#       |
|**`Game Engine`**|Unity    |
|**`Assets`**|[**QuarterView 3D Action assets pack**](https://assetstore.unity.com/packages/3d/characters/quarter-view-3d-action-assets-pack-188720), [**Baker's House**](https://assetstore.unity.com/packages/3d/environments/fantasy/baker-s-house-26443)|
|**`etc`**|[**Icograms 3D Map Online Designer**](https://icograms.com/designer#)|

<br/>

## Development Process
![image](https://user-images.githubusercontent.com/80614927/143541718-e67247c1-23ce-43c8-b92a-25be5cf77c3b.png)   
![image](https://user-images.githubusercontent.com/80614927/143541890-0fba933a-0e14-441c-bae7-30d2629e9483.png)   
![image](https://user-images.githubusercontent.com/80614927/143541997-3084240d-d72e-44f4-920c-27f7970f8311.png)   
![image](https://user-images.githubusercontent.com/80614927/143542049-d86228b4-4819-42c4-a130-e42c1822783c.png)   

<br/>

## InGame
* [**scenario**]
![image](https://user-images.githubusercontent.com/80614927/193030683-04b77ebf-27ad-4950-aa25-f2432809b9e6.png)
* [**UI**]
![image](https://user-images.githubusercontent.com/80614927/143677832-ed422251-b684-41bf-b5f9-12311ac67416.png)
* [**Player UI**]
  - [**Prototype**]
![image](https://user-images.githubusercontent.com/80614927/193110132-1093e233-c873-481c-b41d-cfbc318f96e0.png)
  - [**Build**] 
![image](https://user-images.githubusercontent.com/80614927/143677742-8d85dd7f-57bb-4f65-ba9c-dad540f22ff0.png)
> __1. HP__
> <br/> GameManager 컴포넌트 안에서 player object에 HP 계산이 끝난 뒤 즉, 프레임에 끝에 HP가 적용되야 하기때문에 lateupdate에서 
> <br/> 다음과 같이 UI text에 player object에 health 멤버 변수를 업데이트합니다.
> <br/> 체력 UI 그림 역시 상술한 이유와 같이 GameManager 컴포넌트 안에서 lateupdate에서 다음과 같이 빨간색인 PlayerHealthBar Panel과
> <br/> 하얀색 HealthBar Panel를 겹처서 PlayerHealthBar Panel이 크기를 줄이는 방식을 사용하였습니다.
 ```
 // text 부분
  playerHealthTxt.text = player.health +"/";
```
```
// 그림 UI 부분(Panel)
  PlayerHealthBar.localScale = new Vector3((float)player.health / 100, 1, 1);
```
> __2. 소지GOLD__
> <br/> 소지 Gold도 상술한 이유로 GameManager에 lateupdate에서 다음과 같이 업데이트 하였습니다.
```
 cointext.text = player.Gold.ToString();
```
> __3. 흭득한 열쇠__
> <br/> 흭득한 열쇠도 상술한 이유로 GameManager에 lateupdate에서 다음과 같이 업데이트 하였습니다.
> <br/> RED,BLUE,GREEN 던전에 보스 몬스터를 클리어 하면 다음 던전으로 이동할 수 있는 열쇠를 드롭하는데 해당 열쇠가 가지고 있는 collider와 PlayerObject가 상호작용 시
> <br/> player object에서 해당 열쇠 object가 가지고 있는 Item Script에 멤버 변수인 value 값을 가져와 hasKeys에 해당 value 배열 값을 true로 만들면
> <br/> lateupdate에서 비어 있는 이미지를 가진 object인 whitekey object를 비활성화 시키고 얻은 열쇠의 이미지에 해당하는 이미지를 활성화 하여 업데이트 하였습니다.
> <br/> 해당 hasKeys 배열은 나중에 던전 입구에서 던전으로 들어가는 로직에서도 활용하였습니다.
```
// 플레이어 상호작용
    void Interaction()
    {
        if (eDown && nearObject != null && !isDodge && !isJump && !isDead)
        {
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
```
```
 void LateUpdate()
 {
 if (player.hasKeys[0] == true)
  {
      whitekey1_img.gameObject.SetActive(false);
      greenkey_img.gameObject.SetActive(true);
  }
 if (player.hasKeys[1] == true)
  {
      whitekey2_img.gameObject.SetActive(false);
      bluekey_img.gameObject.SetActive(true);
  }
 if (player.hasKeys[2] == true)
  {
      whitekey3_img.gameObject.SetActive(false);
      redkey_img.gameObject.SetActive(true);
  }
 }
```
> __4. 플레이 타임__
> <br/>GameManger에서 프레임 마다 해당 시간을 구해주면서 프레임 마지막에 시간을 적용 시켜 text UI에 셋팅하였습니다.
```
// GameManager Update 부분
 void Update()
 {
     playTime += Time.deltaTime;
 }
```
```
// GameManager lateupdate 부분
 void LateUpdate()
 {
      int hour = (int)(playTime / 3600);
      int min = (int)((playTime - hour * 3600) / 60);
      int second = (int)(playTime % 60);
      
      timetext.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
 }
```
> __5. 플레이 아이템__
> <br/> 체력 포션 UI도 상술한 이유와 같이 GameManager에서 lateupadte에서 다음과 같이 업데이트하도록 하였습니다.
> <br/> 현재 무기 UI 같은 경우 player script가 가지고 있던 profession_num이라는 gameobject 배열에 player object에 부착 시켜둔 무기 object들을 담아 놓고
> <br/> Z키를 눌러 무기를 스왑하는데 이때 바뀐 무기에 해당하는 이미지와 텍스트를 업데이트합니다.
> <br/> hasWeapons 같은 경우는 후술할 tutorial script와 던전에서 각각 해당 무기를 얻고 난뒤에 swap이 가능해지게 만들기 위해 현재 내가 가지고 있는 무기를 확인하기
> <br/> 위한 배열입니다.
> <br/> Z키를 누르면 현재 장착한 무기의 인덱스보다 한단계 높은 인덱스를 player script에 profession이라는 멤버 함수에 매개 변수로 전달하고
> <br/> 해당 함수에서 그 인덱스에 맞는 profession_num에 넣어둔 gameobject를 활성화 시키는 구조입니다.
```
// 체력 포션 UI
 void LateUpdate()
 {
   hppotiontext.text = "X " + player.HpPotion.ToString();
 }
```
```
// 무기 스왑
public void Swap()
    {
        if (zDown)
        {
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
```
```
// player script 안에서 무기 object가 바뀌는 과정
public void profession(int num)
    {
        if (!isJump && !isDodge && moveVec == Vector3.zero && !isDead)
        {
            
            if (profession_player != null)
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
```
```
// 현재 무기 UI
  void LateUpdate()
  {
    if (player.equip == 0)
    {
      Gunimg.gameObject.SetActive(false);
      Guntxt.gameObject.SetActive(false);
      hammerimg.gameObject.SetActive(true);
      hammertxt.gameObject.SetActive(true);
    }
    else if (player.equip == 1)
    {
      hammerimg.gameObject.SetActive(false);
      hammertxt.gameObject.SetActive(false);
      Gunimg.gameObject.SetActive(true);
      Guntxt.gameObject.SetActive(true);
    }
 }
```
* [**Player Logic**]
  - [**튜토리얼**]
   ![image](https://user-images.githubusercontent.com/80614927/193109874-3602856b-8dc5-4376-8259-eb8d6e3e4080.png)
><br/>1. GameManager에서 GameStart 함수가 실행되면서 시작됩니다. 이때 PanelUI의 animation과 textUI를 셋팅 시켜 해당 조건을 만족할 경우 다음 tutorial을 불러오게 끔합니다. 
><br/>2. tutorial 함수에서는 항상 해당 조건이 완료되었는지 모르니 tu_next라는 bool 멤버 변수를 주어 해당 변수가 true가 되면 GameManager에 LateUpdate로 하여금 
><br/>다음 튜토리얼 진행하게 끔합니다.
><br/>3. 기본 조작법이 끝나면 다음으로 나오는 tutorial들은 해당 object와의 상호 작용을 통해 조건 완수를 알수 있으므로 tu_next함수는 더 이상 사용되지 않습니다.
```
  public void GameStart()
    {
        TutorialPanel.SetBool("isShow", true);
        Tutorial(1);
    }
```
```
// GameManager Tutorial 함수
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
            player.equip = 0;
            player.profession(player.equip);
            player.hasWeapons[player.equip] = true;
            hammerimg.gameObject.SetActive(true);
            hammertxt.gameObject.SetActive(true);
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
```
```
// gamemanager LateUpdate
void LateUpdate()
{
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
  }
```
* [**InDungeon Logic**]
  - [**던전 입장 로직**]
   ![image](https://user-images.githubusercontent.com/80614927/193107499-d00f28bf-5915-49ad-9c0e-79e1238e8678.png)
   ><br/> Dunenter라는 tag를 가진 collider가 있는 곳에서 player object가 interaction을 할 경우
   ><br/> 기존에 생성해놓은 panelUI가 애니메이션과 함께 사용자의 화면에 등장하고
   ><br/> 이때 이 패널 button에 저장해놓은 player 함수를 실행하면 해당 던전에 시작에 해당하는 좌표로 player object에 좌표를 이동시킵니다.
  ```
  void interaction()
  {
    ...
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
    ...
  }
  ```
  ```
  // panel에 심은 player 함수
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
  ```
  ```
  // Dunenter class 안에 멤버 함수들
  public void GreenEnter()
    {
        TutorialPanel.SetBool("isShow", false);
        uiGroup1.anchoredPosition = Vector3.zero;

    }

    public void GreenExit()
    {
        uiGroup1.anchoredPosition = Vector3.down * 2000;
    }
  ```
  - [**던전 로직**]
   ![image](https://user-images.githubusercontent.com/80614927/193107586-3a49574e-d71c-4597-b3b0-76493386fdcf.png)
   ><br/> 던전안에 각 스테이지 입구앞에 collider component를 가진 gameobject을 설치하여 해당 gameobject안에 ontriggerEnter에서 충돌된 물체가 player이면
   ><br/> gamemanager 안에서 StageStart 멤버 함수를 실행시킵니다. 이때 매개변수로 가져간 숫자에 따라 각각의 스테이지에 따른 로직을 작동시킵니다.
   ><br/> 스테이지에 공통된 로직은 plyaer object가 진입 시 그 로직이 다시 실행되지 않게 우선 player object가 충돌 이벤트를 일으키는 상술한 gameobject를 비활성화 시키고
   ><br/> 플레이어가 해당 스테이지에서 잠금 조건을 해제할때까지 나가지 못하게 미리 설치해둔 벽 object들을 활성화 시킵니다.
   ><br/> 던전 로직 중 하나를 예시로 들자면 해당 스테이지에 리스폰 된 몬스터들을 모두 해치우면 해당 stage가 끝나는 로직이 있습니다.
   ><br/> 몹을 리스폰 할 때 gamemanger에 멤버 변수인 enemyCnt에 현재 소환된 몹들의 수를 담아두고 lateupdate에서 enemycnt를 확인합니다. 
   ><br/> 이때 스테이지 진입전에는 항시 enemyCnt가 0이니까 하나 더 조건을 추가합니다. 현재 배틀중인지를 확인하는 IsBattle 멤버변수를 추가하여 StageStart 함수에서
   ><br/> true로 변환하여 lateupdate에서 확인합니다. 이때 enemyCnt가 0이 되고 현재 isBattle이 true이면 stageEnd 함수를 실행합니다.
   ><br/> stageEnd 함수는 상술한 각각의 스테이지를 확인하는 숫자를 조건으로 if문을 통해 실행합니다.
  '''
  // 스테이지 입구에 있는 gameobject OnTriggerEnter함수
  public class EnterLock : MonoBehaviour
{
    public GameManager manager;

    void OnTriggerEnter(Collider other)
    {
        int gate = 1;
        if (other.gameObject.tag == "Player")
            manager.StageStart(gate);
    }
}
  '''
  '''
  public void StageStart(int gate)
  {
        if (gate == 0)
        {
            //tutorial
            gate_num = 0;
            GameObject instantEnemy = Instantiate(enemies[0], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = player.transform;
            enemy.gamemanager = this;
            isBattle = true;
            enemyCnt++;
        }
        else if (gate == 1)
        {
            //green room1
            gate_num = 1;
            LockTrigger.SetActive(false);
            EnterLock1.SetActive(true);
            EnterLock2.SetActive(true);
            Debug.Log("진입");
            for (int MobCount = 1; MobCount < 5; MobCount++)
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
  '''
  '''
  //
  void LateUpdate()
  {
   if (enemyCnt<=0 && isBattle==true)
   {
            StageEnd();
   }
  }
  '''
  '''
  public void StageEnd()
  {
        if (gate_num == 0)
        {
            TutorialPanel.SetBool("isShow", false);
            isBattle = false;
        }
  }
  '''
  - [**어그로 로직**]
   ![image](https://user-images.githubusercontent.com/80614927/193107878-efa5b1de-7f1d-4625-b873-dc418535dbf5.png)


<br/>

## MapDesign
* [**Prototype**]
![image](https://user-images.githubusercontent.com/80614927/193028127-41d00cc4-e043-4ade-9637-5d2feaf6f3ef.png)
![image](https://user-images.githubusercontent.com/80614927/193028280-d92cbf6b-3fbe-4fdb-b005-1e8791d5a242.png)
![image](https://user-images.githubusercontent.com/80614927/193028409-7defdcf7-aa3e-40dd-a59a-9192f1ca2ad3.png)
![image](https://user-images.githubusercontent.com/80614927/193028439-f3bc0477-da83-468e-bc25-2892e1c9c828.png)
* [**Build**]
![image](https://user-images.githubusercontent.com/80614927/193028466-8ff1713b-858f-4bd4-a9ee-9b3328b024bd.png)
![image](https://user-images.githubusercontent.com/80614927/193028488-dca8846b-f4a8-4fdb-b151-27f4ec8589d4.png)
![image](https://user-images.githubusercontent.com/80614927/193028515-27f1571e-9f0f-4931-a578-c28097380c6a.png)
![image](https://user-images.githubusercontent.com/80614927/193028541-30508387-7f7c-4d10-b84f-6b464327d961.png)
<br/>
