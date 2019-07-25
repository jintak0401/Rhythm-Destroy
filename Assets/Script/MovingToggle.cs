using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToggle : MonoBehaviour
{
  public Vector3 velocity;                     // 이동 속도
  private Vector3 position;                   // 회색 이동 토글의 초기 위치
  public WhiteToggle[] whiteToggles;         // Bar 위에 있는 Toggle 들
  public State state;                        // strategy -> input -> run -> strategy 바꿔주기 위함
  public Round round;                         // round 바꿔주기 위함
  private int index;                         // whiteToggles 몇 번 째인지 나타내는 index
  public float difficulty;                  // 입력 타이밍 간격. 작을수록 어려움
  public InputManager inputManager;         // 입력을 맡는 게임 오브젝트
  private float bluebarLength;              // 초기 막대의 총 길이 (회색 토글 이동을 위함)
  private SpriteRenderer greenRenderer;     // 초록색 막대의 실시간 길이를 알기 위해 선언
  private float lastPositionX;              // grayToggle 이 처음으로 되돌아갔는지 판단하기 위한 전 프레임의 X 좌표

  void initializeWhiteToggleCorrect()         // white toggle 들의 초기 correct 설정
  {
    for (int i = 0; i < whiteToggles.Length; i++)
    {
      whiteToggles[i].correct = false;
    }
  }                   

  void initializeWhiteToggleDone()                  // white Toggle 들의 초기 done 설정
  {
    for (int i = 0; i < whiteToggles.Length; i++)
    {
      whiteToggles[i].done = false;
    }
  }                     

  void initializeWhiteTogglePosition()                      // whiteToggle 들의 초기 위치 설정
  {
    float gap = bluebarLength / (whiteToggles.Length + 1);
    for (int i = 1; i <= whiteToggles.Length; i++)
    {
      whiteToggles[i - 1].transform.position = new Vector3(position.x + i * gap, position.y, -3); 
    }
  }

  void setWhiteToggleCorrect()                              // 입력시 각 whiteToggle 들의 correct 설정
  {
    if ((inputManager.getTouch() || inputManager.getInput()) && index < whiteToggles.Length)
    {
      float diff = this.transform.position.x - whiteToggles[index].transform.position.x;
      if (diff < -2)  // 늦게 입력했을 때의 보정
      {
        return;
      }
       else if (diff > - 1 * difficulty && diff < 2 * difficulty)  // 올바른 타이밍에 입력 && 입력시 타이밍 보정
      {
        whiteToggles[index].done = true;
        whiteToggles[index].correct = true;
        inputManager.printing();
      }
      else    // 잘못된 타이밍에 입력
      {
        whiteToggles[index].done = true;
        whiteToggles[index].correct = false;
        inputManager.inputNum[inputManager.index] = 0;
        inputManager.printing();
      }
      whiteToggles[index++].appearCorrect();
      inputManager.index++;
    }
    else if (index < whiteToggles.Length && !whiteToggles[index].done && this.transform.position.x - whiteToggles[index].transform.position.x >= difficulty)     
    {     // 해당 토글입력 시간 내에 입력하지 못 함
      whiteToggles[index].done = true;
      whiteToggles[index].correct = false;
      inputManager.inputNum[inputManager.index] = 0;
      inputManager.printing();
      inputManager.index++;
      whiteToggles[index++].appearCorrect();
    }
  }

  void disappearWhiteToggleCorrect()      // 실행 state 일 때 O, X 사라지게하는 함수 호출
  {
    if (index < whiteToggles.Length &&  this.transform.position.x > whiteToggles[index].transform.position.x)
    {
      whiteToggles[index].done = false;
      whiteToggles[index++].disappearCorrect();
    }
  }

  private void initializeGrayTogglePosition()     // 초기 grayToggle 의 위치 설정, bluebarLength 초기화, greenRenderer 초기화
  {
    GameObject blue = GameObject.Find("BlueBar");
    SpriteRenderer blueRenderer = blue.GetComponent<SpriteRenderer>();
    bluebarLength = blueRenderer.bounds.size.x;
    greenRenderer = GameObject.Find("ForGreenBar/GreenBar").GetComponent<SpriteRenderer>();
    position = new Vector3(-0.5f * bluebarLength, blue.transform.position.y, -4);
  }

  void moveGrayToggle()
  {
    this.transform.position = new Vector3(position.x + bluebarLength - greenRenderer.bounds.size.x, this.transform.position.y, this.transform.position.z);
  }

  // Start is called before the first frame update
  void Start()
    {
    initializeGrayTogglePosition();
    index = 0;
    initializeWhiteToggleCorrect();
    initializeWhiteTogglePosition();
    for (int i = 0; i < whiteToggles.Length; i++)
    {
      whiteToggles[i].makeSymbol();
    }
    lastPositionX = -20;  // 맨 처음이 전략 state 이기 위해 임의로 -20 설정
    
  }

    // Update is called once per frame
    void Update()
    {
    if (GameObject.Find("Start") == null)
    {
      moveGrayToggle();       // grayToggle 이동

      if (round.chapter == 1)   // 입력 state 일 때
      {
        setWhiteToggleCorrect();
      }

      else if (round.chapter == 2)    // 실행 state 일 때
      {
        disappearWhiteToggleCorrect();
      }

      if (this.transform.position.x < lastPositionX)    // 맨 처음으로 이동했을 때
      {
        this.transform.position = position;
        index = 0;
        initializeWhiteTogglePosition();
        state.StartCoroutine(state.change());
        round.chapter++;
        if (round.chapter == 3)
        {
          round.chapter = 0;
          round.round++;
          round.StartCoroutine(round.changeRound());
          inputManager.index = 0;
        }
      }
      lastPositionX = this.transform.position.x;  // 매번 해당 프레임의 grayToggle 의 x 좌표를 저장
    }
    }
}
