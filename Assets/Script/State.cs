using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
  // Start is called before the first frame update

  private SpriteRenderer spriteRenderer;             // sprite 변경을 위함
  public Sprite[] states;                            // 전략, 입력, 실행 sprite
  private int index;                                  // 나머지가  < 0 일 때 : 전략  | 1 일 때 : 입력  |  2 일 때 : 실행 >
  public float speed;                                // 회전속도


  private WaitForSeconds wait = new WaitForSeconds(0.01f);        // 코루틴에서 쓰기 위함

    void Start()
    {
    index = 0;
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = states[0];                               // 전략 state 로 시작
    }

    public IEnumerator change()
  {
    float angle;    // 몇 도씩 회전할 것인지
    float sum = 0;
    while (sum < 90)
    {
      angle = speed * Time.deltaTime;
      this.transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);               // 회전
      sum += angle;
      yield return wait;
    }
    this.transform.rotation = Quaternion.Euler(90, 0, 0);
    spriteRenderer.sprite = states[++index % 3];    // 다음 state 로 sprite 변경
    sum = 0;
    while (sum < 90)
    {
      angle = speed * Time.deltaTime;
      this.transform.rotation *= Quaternion.AngleAxis(-1 * angle, Vector3.right);            // 회전하며 원래 각도로 복구
      sum += angle;
      yield return wait;
    }
    this.transform.rotation = Quaternion.Euler(0, 0, 0);
    yield break;
  }

    // Update is called once per frame
    void Update()
    {
    
    }
}
