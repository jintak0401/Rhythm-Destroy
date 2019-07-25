using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycle : MonoBehaviour
{
  // Start is called before the first frame update
  public Sprite[] cycle;
  private int index;
  public GameObject obj;
  //private Animator ani;
  private SpriteRenderer spriteRenderer;

  

    void Start()
    {
    index = 0;
   // ani = GetComponent<Animator>();
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = cycle[index];
    }

    // Update is called once per frame
    void Update()
    {
    float key = 200 * Time.deltaTime;
    Debug.Log(key);
    obj.transform.rotation *= Quaternion.AngleAxis(key, Vector3.up);
  }
  
  }


