# Avoid Rocket

## 게임 소개
```
- 플레이어는 우주선을 조이스틱으로 움직이면서 날아오는 로켓들을 피하는 간단한 미니 게임이다.
- 일정 시간마다 로켓들이 다수 출현하는 로켓붐 시간이 있다. 이때는 경고음과 화면의 깜빡임 연출로 플레이어에게 경고를 준다.
- 로켓들을 피하며 최대한 오래 버티는 게임으로 우주선이 로켓에 격추되어 게임이 종료될 시 버틴 시간이 나온다.
- 게임의 첫 화면으로 돌아갈 수 있는 버튼을 누르고 재 시작을 할 수 있다.
```

## 코드 기법

* #### 싱글턴 패턴
  - 고유한 MasterManager를 통해서만 각 매니저 컴포넌트들을 호출할 수 있다.

* #### 옵저버 패턴
  - Action을 이용하여 각 이벤트 발생 시 각 함수에게 알려준다.

* #### 오브젝트 풀링
  - 게임에서는 30개의 다양한 로켓들이 등장하며, 각 로켓들은 기본 10개씩의 로켓들이 생성되며, 스택으로 관리한다.

* #### 코루틴
  - 지정 시간마다 로켓들을 랜덤 스폰한다.
  - 로켓 붐 시간때 화면 깜빡임을 코루틴을 이용해 SetActive를 껐다켰다 하는 식으로 구현하였다.
    (조이스틱의 이동에 제한을 받지 않게 하기 위해 배경과 조이스틱 사이에 배치하였다.)

## 게임 제작 후 배운 점
```
첫 게임을 만드는 과정에서 단순히 게임의 기능을 구현한다기보다는 구조를 정확히 파악하고 코드의 유지보수성을 위해 클래스와 함수로
기능을 명확히 구분하도록 하였다. 이후 기능을 추가하거나 수정할 때 확실히 코드의 수정이나 추가가 쉬웠고, 이 구조를 통해 다른 게임의
구조로도 사용하여 다양한 게임을 만들 수 있을 것 같다.
```
    
* #### 지정 축 기준으로 이동하는 법
  - 로켓이 발사되면서 LookAt()을 통해 로켓의 바라보는 방향을 변경하였는데 해당 로켓은 y축이 기준이 되어야 했다.
    transfrom.up을 방향 벡터로 지정해주면 y축이 해당방향을 바라보며 간다는 것을 알았고 다른 축도 응용이 가능함을 알았다.
    
* #### 화면 비율에 따른 스크린 좌표와 캔버스 좌표
  - 해상도에 따라 화면 비율을 맞추기 위해 UI 스케일 모드를 Scale With Screen Size로 돌린 후 조이스틱이 이상동작하였다.
    스크린 좌표를 캔버스 좌표로 변환해주는 함수를 통해 해결하였다.
    
* #### Collider 제한하는 법
  - 기존에는 해상도를 고려하지 않아서 플레이어의 이동 제한을 스크린 외부에 벽을 두어 제한하였다. 플레이어는 벽을 통과할 수 없고
    로켓은 벽을 통과할 수 있도록 하기 위해 각각 레이어를 지정 후 따로 제한을 두면 된다는 점을 알았다.
  - 현재는 위의 방법을 쓰지 않고 스크린의 좌표를 가져와서 플레이어의 이동 가능 경로를 제한하는 식으로 변경하였다.
    
* #### 오브젝트 풀링 시 초기화 주의
  - 오브젝트 풀링 기법을 이용하면서 게임의 시작 시간이 지남에 따라 로켓의 목표 지점이 이상하여 코드를 유심히 들여다보니
    초기화 하는 부분이 풀링을 이용하면서 되지 않았기에 풀링을 이용하여 로켓을 활성화할 경우 OnEnable()을 통해 초기화를 해주었다.


## 게임 화면
<img width="80%" src="https://user-images.githubusercontent.com/37278829/156876690-b43b7a20-bc2a-424b-b213-483368479617.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/156876691-f9b40a52-a944-45a0-9e48-36e9ac12e2e7.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/156876696-06b51de2-8b62-4048-b7a9-6ebff52e3e0b.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/156876698-bd266502-6807-4632-a24c-1584f5eb73d9.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/156876701-af8b333e-60e4-445d-bef3-3eaaacad6c81.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/156876702-242053b0-98c6-46c1-8a1d-249d081df5cb.png"/>

## 게임 시작 화면 수정(볼륨 설정 및 게임 종료 추가)
<img width="80%" src="https://user-images.githubusercontent.com/37278829/157395431-64bd3441-eb5d-4bf2-b6f7-4c5f7200e4b4.png"/>
<img width="80%" src="https://user-images.githubusercontent.com/37278829/157395437-20b369de-4f8e-4471-8c14-449266a3779e.png"/>
