using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{

  public int round;                                // 현재 round  (MovingToggle 에서 바꿔준다)
  private SpriteRenderer spriteRenderer;            // 이미지 바꿔주기 위함
  public Sprite[] Rounds;                           // 라운드에 따라 바꿔줄 sprite
  public int chapter;                               // 0 일 때 : 전략  | 1 일 때 : 입력  |  2 일 때 : 실행  (MovingToggle 에서 바꿔준다)
  public float speed;

  public IEnumerator changeRound()   // 라운드에 따라 회전하며 이미지 변경                                                          
  {
    float angle;
    float sum = 0;
    while (sum < 90)
    {
      angle = Time.deltaTime * speed;
      this.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);                //  자전하듯 회전
      sum += angle;
      yield return null;
    }
    this.transform.rotation = Quaternion.Euler(0, 90, 0);
    spriteRenderer.sprite = Rounds[round - 1];    // sprite 변경
    sum = 0;
    while (sum < 90)
    {
      angle = Time.deltaTime * speed;
      this.transform.rotation *= Quaternion.AngleAxis(-1 * angle, Vector3.up);            // 회전하며 원래 각도로 복구
      sum += angle;
      yield return null;
    }
    this.transform.rotation = Quaternion.Euler(0, 0, 0);
    yield break ;
  }

  

    // Start is called before the first frame update
    void Start()
    {
    round = 1;
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();   // sprite 변경을 위해 해줌         
    chapter = 0;
  }

    // Update is called once per frame
    void Update()
    {

    }
}
