using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
  public GameObject[] inputNum;
  private GameObject obj;
  public float growSpeed;
  public float shrinkSpeed;
  public float maxSize;
  public float keepedSize;

  public void initializePosition(Vector3 position)
  {
    this.transform.position = position;
  }

  public void appearDisplay(int index)
  {
    if (index == 0)
    {
      return;
    }
    StartCoroutine(appear(index));
  }

  public void disappearDisplay()
  {
    if (obj == null)
    {
      return;
    }
    StartCoroutine(disappear());
  }

  public IEnumerator appear(int index)
  {
    obj = Instantiate(inputNum[index], this.transform.position, this.transform.rotation) as GameObject;
    obj.transform.localScale = new Vector3(0, 0, 1);

    float sum = 0;
    float speed;
    while (sum < maxSize)
    {
      speed = growSpeed * Time.deltaTime;
      sum += speed;
      obj.transform.localScale += new Vector3(speed, speed, 0);                // maxSize 가 되도록 커지게 크기 변화
      yield return null;
    }
    while (sum > keepedSize)
    {
      speed = shrinkSpeed * Time.deltaTime;
      sum -= speed;
      obj.transform.localScale -= new Vector3(speed, speed, 0);                // maxSize 에서 keepedSize 까지 크기변화
      yield return null;
    }
    obj.transform.localScale = new Vector3(keepedSize, keepedSize, 1);
  }

  public IEnumerator disappear()                                                        // 실행 state  일 때 사라지는 애니메이션
  {
    float speed;
    float sum = obj.transform.localScale.x;
    while (sum < maxSize)
    {
      speed = shrinkSpeed * Time.deltaTime;
      sum += speed;
      obj.transform.localScale += new Vector3(speed, speed, 0);                  // keepedSize 에서 maxSize 가 되도록 커지게 크기 변화
      yield return null;
    }
    while (sum > 0)
    {
      speed = growSpeed * Time.deltaTime;
      sum -= speed;
      if (sum < 0)
      {
        obj.transform.localScale = new Vector3(0, 0, 1);
      }
      else
      {
        obj.transform.localScale -= new Vector3(speed, speed, 0);                        // maxSize 에서 0 이 되도록 크기변화
      }
      yield return null;
    }
    obj = null;
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
