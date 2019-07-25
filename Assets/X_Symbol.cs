using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_Symbol : MonoBehaviour
{
  public float keepedSize;           // 마지막에 유지될 사이즈
  public float maxSize;              // 가장 클 때의 사이즈
  public float growSpeed;            // 나타날 때 자라는 속도 && 사라질 때 작아지는 속도
  public float shrinkSpeed;           // 나타날 때 maxSize 에서 keepedSize 로 작아질 대 속도 && 사라질 때 keepedSize 에서 maxSize 로 커지는 속도


  // Start is called before the first frame update
  void Start()
  {
    this.transform.localScale = new Vector3(0, 0, 1);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public IEnumerator appear()                                                   //  틀렸을 때 X 가 나오는 애니메이션
  {
    float sum = 0;
    while (sum < maxSize)
    {
      sum += growSpeed * Time.deltaTime;
      this.transform.localScale += new Vector3(growSpeed * Time.deltaTime, growSpeed * Time.deltaTime, 0);            // maxSize 가 되도록 커지게 크기 변화
      yield return null;
    }
    while (sum > keepedSize)
    {
      sum -= shrinkSpeed * Time.deltaTime;
      this.transform.localScale -= new Vector3(shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime, 0);         // maxSize 에서 keepedSize 까지 크기변화
      yield return null;
    }
    this.transform.localScale = new Vector3(keepedSize, keepedSize, 1);
    yield break;

  }

  public IEnumerator disappear()                                                   // 실행 state 일 때 사라지는 애니메이션
  {
    float sum = this.transform.localScale.x;
    while (sum < maxSize)
    {
      sum += shrinkSpeed * Time.deltaTime;
      this.transform.localScale += new Vector3(shrinkSpeed * Time.deltaTime, shrinkSpeed * Time.deltaTime, 0);      // keepedSize 에서 maxSize 가 되도록 커지게 크기 변화
      yield return null;
    }
    while (sum > 0)
    {
      sum -= growSpeed * Time.deltaTime;
      if (sum < 0)
      {
        this.transform.localScale = new Vector3(0, 0, 1);
      }
      else
      {
        this.transform.localScale -= new Vector3(growSpeed * Time.deltaTime, growSpeed * Time.deltaTime, 0);            // maxSize 에서 0 이 되도록 크기변화
      }
        yield return null;
    }
    yield break;
  }
}
