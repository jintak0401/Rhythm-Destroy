using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBar : MonoBehaviour
{
  public Vector3 velocity;
  Vector3 initLength;

  // Start is called before the first frame update
    void Start()
    {
    initLength = transform.localScale;
  }

  // Update is called once per frame
  void Update()
  {
    if (GameObject.Find("Start") == null)
    {
      transform.localScale -= velocity * Time.deltaTime;

      if (transform.localScale.x <= 0)
      {
        transform.localScale = initLength;
      }
    }
    //Debug.Log(GameObject.Find("ForGreenBar/GreenBar").GetComponent<SpriteRenderer>().bounds.size.x);
  }
}
