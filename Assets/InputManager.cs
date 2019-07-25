using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public InputCollider[] obj;      // Input[1...]
  private InputCollider target;    // rayCast 의 target
  private Vector3 leftDown;     // 게임화면의 왼쪽 아래 월드 좌표 (콜라이더 크기 및 위치 정하기 위함)
  private Vector3 rightUp;      // Round 밑의 오른쪽 위 월드 좌표 (콜라이더 크기 및 위치 정하기 위함)
  public int[] inputNum = new int[4];    // 선택된 콜라이더
  public int index;       // 몇 번째 입력인지에 대한 index (MovingToggle 에서 변경해줌)
  private string[] possibleKey = { "a", "s", "d", "f", "j", "k", "l", ";" };      // 컴퓨터로 플레이시 콜라이더 선택 가능한 키들 (왼 쪽부터 1 ~ 8)
  public WhiteToggle lastWhiteToggle;
  private Coroutine[] fadeCoroutine;

  private int selected_Collider(string inputKey)      // 몇을 입력했는지 표시 & 입력될 숫자 반환
  {
    int toReturn = 0;
    for (int i = 0; i< inputKey.Length; i++)
    {
      int tmp = inputToNum(inputKey[i].ToString());
      if (tmp != 0){
        if (toReturn == 0)
        {
          toReturn = tmp;
        }
        fadeCoroutine[tmp - 1] = obj[tmp - 1].StartCoroutine(obj[tmp - 1].fade(fadeCoroutine[tmp - 1]));
      }
    }
    return toReturn;
  }

  public bool getInput()      // 입력 키를 index 에 맞게 inputNum 에 넣어줌
  {
    string inputKey = Input.inputString;    // 해당 프레임에서 입력받은 String
    int selectedCollider = selected_Collider(inputKey);
    if (selectedCollider == 0)
    {
      return false;
    }
    else
    {
      if (index < inputNum.Length)
      {
        inputNum[index] = selectedCollider;
      }
      return true;
    }
  }

  public int inputToNum(string inputKey)
  {
    switch (inputKey.ToLower())
    {
      case "a":
        return 1;
      case "s":
        return 2;
      case "d":
        return 3;
      case "f":
        return 4;
      case "j":
        return 5;
      case "k":
        return 6;
      case "l":
        return 7;
      case ";":
        return 8;
      default:
        return 0;
    }
  }


  private void setInputNum(int num)
  {
    if (index < inputNum.Length)
    {
      inputNum[index] = num;
    }
  }

  /* public bool getTouch()
  {
    if (Input.touchCount > 0)
    {
      Touch touch = Input.GetTouch(0);
       if (touch.phase == TouchPhase.Began)
       {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
          inputNum[index] = hit.collider.name[hit.collider.name.Length - 1] - '0';
          return true;
        }
      }
    }
    return false;
  } */

  public bool getTouch()
  {
    bool toReturn = false;
    int count = Input.touchCount;
    for (int i = 0; i < count; i++)
    {
      Touch touch = Input.GetTouch(i);
      if (touch.phase == TouchPhase.Began)
      {
        Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position /*Input.mousePosition */);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
          target = hit.collider.GetComponent<InputCollider>();
          int selectedCollider = target.name[target.name.Length - 1] - '0';
          fadeCoroutine[selectedCollider - 1] = target.StartCoroutine(target.fade(fadeCoroutine[selectedCollider - 1]));
          if (i == 0)
          {
            if (index < inputNum.Length)
            {
              inputNum[index] = selectedCollider;
              toReturn = true;
            }
          }
        }
      }
    }
    return toReturn;
  }

  public void printing()   // 제대로 입력됐는지 확인용
  {
    Debug.Log("inputNum[" + (index) + "] : " + inputNum[(index)]);
  }

  void initializeCollider()          // 초기 콜라이더 간격에 맞게 배치
  {
    leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));    // 게임화면의 왼쪽 아래 월드 좌표
    rightUp = new Vector3(-1 * leftDown.x, GameObject.Find("Round").transform.position.y - 0.6f, 0);      // Round 밑의 오른쪽 위 월드 좌표
    float widthGap = (rightUp.x - leftDown.x) / (obj.Length);       // 콜라이더 가로길이의 절반  
    float heightGap = (rightUp.y - leftDown.y) / 4;                 // 콜라이더 세로길이의 절반

    for (int i = 0; i < obj.Length / 2; i++)      // 콜라이더 위치와 크기 설정
    {
      obj[i].transform.position = new Vector3(leftDown.x + (2 * i + 1) * widthGap, rightUp.y - heightGap, -9);
      obj[i].transform.localScale = new Vector3(2 * widthGap, 2 * heightGap, 1);
      obj[i].initialPosition(2 * widthGap, 2 * heightGap);
      obj[(obj.Length / 2) + i].transform.position = new Vector3(leftDown.x + (2 * i + 1) * widthGap, leftDown.y + heightGap, -9);
      obj[(obj.Length / 2) + i].transform.localScale = new Vector3(2 * widthGap, 2 * heightGap, 1);
      obj[(obj.Length / 2) + i].initialPosition(2 * widthGap, 2 * heightGap);
    }
  }
  
  public void initializeFadeCoroutine()
  {
    fadeCoroutine = new Coroutine[obj.Length];
    for (int i = 0; i < fadeCoroutine.Length; i++)
    {
      fadeCoroutine[i] = null;
    }
  }

    // Start is called before the first frame update
    void Start()
    {
    index = 0;
    initializeFadeCoroutine();
    initializeCollider();   // 콜라이더 위치 초기화

  }

    // Update is called once per frame
    void Update()
    {
    
    }
}
