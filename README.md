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
> 1. HP
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
> 2. 소지GOLD
> <br/> 소지 Gold도 상술한 이유로 GameManager에 lateupdate에서 다음과 같이 업데이트 하였습니다.
```
 cointext.text = player.Gold.ToString();
```
> 3. 흭득한 열쇠
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
> 4. 플레이 타임
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
> 5. 플레이 아이템
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
  - [**Tutorial Script**]
  - ![image](https://user-images.githubusercontent.com/80614927/193109874-3602856b-8dc5-4376-8259-eb8d6e3e4080.png)

* [**InDungeon Logic**]
  - [**던전 입장 로직**]
  - ![image](https://user-images.githubusercontent.com/80614927/193107499-d00f28bf-5915-49ad-9c0e-79e1238e8678.png)
  - [**던전 로직**]
  - ![image](https://user-images.githubusercontent.com/80614927/193107586-3a49574e-d71c-4597-b3b0-76493386fdcf.png)
  - [**어그로 로직**]
  - ![image](https://user-images.githubusercontent.com/80614927/193107878-efa5b1de-7f1d-4625-b873-dc418535dbf5.png)


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
