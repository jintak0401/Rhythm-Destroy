using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{

  public float numInitialPosition;
  public float startInitialPosition;
  private Vector3 numPosition;
  private Vector3 startPosition;
  public Sprite[] img;
  Color color;
  public float fadeSpeed;
  public float moveSpeed;
  private WaitForSeconds wait = new WaitForSeconds(0.01f);
  private SpriteRenderer spriteRenderer;

 
  IEnumerator fade()
  {
    for (int i = 0; i < 3; i++)                                        // 숫자
    {
      spriteRenderer.sprite = img[i];
      color = this.spriteRenderer.color;
      color.a = 0;
      spriteRenderer.color = color;
      this.transform.position = numPosition;
      while (color.a < 1)                                              // 페이드 인
      {
        this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);       // 오른쪽으로 이동
        color.a += fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;
        yield return wait;
      }
      yield return new WaitForSeconds(0.3f);                            // 가운데에서 잠시 멈춤
      while (color.a > 0)                                               //페이드 아웃
      {
        this.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);        // 오른쪽으로 이동
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;
        yield return wait;
      }
    }
    StartCoroutine(starting());
  }
   
  IEnumerator starting()                                                 //   START 
  {
    spriteRenderer.sprite = img[3];
    color = this.spriteRenderer.color;
    color.a = 0;
    spriteRenderer.color = color;
    this.transform.position = startPosition;
    this.transform.localScale /= 2;
    while (color.a < 1)
    {
      this.transform.position -= new Vector3(0, moveSpeed * 2 * Time.deltaTime, 0);              // 어느정도 속도로 내려올지
      color.a += 3 * fadeSpeed * Time.deltaTime;                                                   // 어느정도 속도로 페이드 인할지
      spriteRenderer.color = color;
      yield return wait;
    }
    yield return new WaitForSeconds(0.8f);
    while (color.a > 0)                                                       //   점점 커지면서 투명해짐
    {
      this.transform.localScale += new Vector3(fadeSpeed * 0.6f * Time.deltaTime, fadeSpeed * 0.6f * Time.deltaTime, 0);              //  어느정도 속도로 커질지
      color.a -= fadeSpeed * Time.deltaTime;               // 어느정도 속도로 페이드 아웃할지
      spriteRenderer.color = color;
      yield return wait;
    }
    Destroy(gameObject);           // 자기 자신 오브젝트 파괴   Destroy(this) -->  오브젝트가 아닌 스크립트 파괴             
  }


  // Start is called before the first frame update
  void Start()
    {
    numPosition = new Vector3(numInitialPosition, -1, -5);
    startPosition = new Vector3(0, startInitialPosition, -5);
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>() ;
    StartCoroutine(fade());
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
