using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbols : MonoBehaviour
{

  //public Sprite[] symbols;
  private Animator ani;

  public void appear()
  {
    ani.SetBool("correct", true);
  }

  public void disappear()
  {
    ani.SetBool("next", true);
  }

  public void zeroSize()
  {
    this.transform.localScale = new Vector3(0, 0, 1);
  }

  public void upSize()
  {
    this.transform.localScale = new Vector3(0.6f, 0.6f, 1);
  }

    // Start is called before the first frame update
    void Start()
    {
    ani = gameObject.GetComponent<Animator>();
    this.transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
             
    }
}
