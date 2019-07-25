using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCollider : MonoBehaviour
{
  public GameObject frame;
  private GameObject ownFrame = null;
  public GameObject num;
  private SpriteRenderer frameRenderer;
  private SpriteRenderer numRenderer;
  public float fadeSpeed;
  private Color frameColor;
  private Color numColor;
  private WaitForSeconds wait = new WaitForSeconds(0.01f);
  //private Coroutine fadeCoroutine;

  public void initialPosition(float width, float height)
  {
    ownFrame = Instantiate(frame, this.transform.position, this.transform.rotation) as GameObject;
    initialRenderer();  // spriteRenderer 할당 ( 색도 투명으로 바꿔준다 )
    num.transform.position = this.transform.position;
    ownFrame.transform.localScale = new Vector3(width / frameRenderer.bounds.size.x, height / frameRenderer.bounds.size.y, 1);   // localScale 이 아닌 실제크기 조정
  }

  public void initialRenderer()
  {
    frameRenderer = ownFrame.GetComponent<SpriteRenderer>();
    numRenderer = num.GetComponent<SpriteRenderer>();
    frameColor = frameRenderer.color;
    numColor = numRenderer.color;
    frameColor.a = 0;
    numColor.a = 0;
    frameRenderer.color = frameColor;
    numRenderer.color = numColor;
  }

  public IEnumerator fade(Coroutine lastCoroutine)
  {
    if (lastCoroutine != null)    // 전에 돌고 있던 코루틴 ( fade 코루틴이 끝나기 전에 다시 호출했을 때 전의 코루틴 종료시켜줌)
    {
      StopCoroutine(lastCoroutine);
    }

    numColor.a = 0;
    frameColor.a = 0;
    frameRenderer.color = frameColor;
    numRenderer.color = numColor;

    while (numColor.a < 1)              // 페이드 인
    {
      numColor.a += fadeSpeed * Time.deltaTime;
      frameColor.a = numColor.a;
      frameRenderer.color = frameColor;
      numRenderer.color = numColor;
      yield return wait;
    }
    
    yield return new WaitForSeconds(0.1f);

    while (numColor.a > 0)            //페이드 아웃
    {
      numColor.a -= fadeSpeed * Time.deltaTime;
      if (numColor.a < 0)
      {
        numColor.a = 0;
        frameColor.a = 0;
        frameRenderer.color = frameColor;
        numRenderer.color = numColor;
        yield break;
      }
      frameColor.a = numColor.a;
      frameRenderer.color = frameColor;
      numRenderer.color = numColor;
      yield return wait;
    }
  }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
  
}
