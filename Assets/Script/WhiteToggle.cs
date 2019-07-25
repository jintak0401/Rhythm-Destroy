using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteToggle : MonoBehaviour
{
  //public Vector3 position;
  static GameObject GrayToggle;
  public Display display;
  public bool correct;
  public bool done;
  public O_Symbol right = null;
  public X_Symbol wrong = null;
  private O_Symbol o;
  private X_Symbol x;
 // public Symbols[] symbol;
  private Animator ani;
  private int index;

  public void makeSymbol()
  {
    o = Instantiate(right, this.transform.position - new Vector3(0, 0, 0.5f), this.transform.rotation) as O_Symbol;
    //UnityEditor.ObjectFactory.CreateInstance(right, this.transform.position, this.transform.rotation);
    o.transform.localScale = new Vector3(0, 0, 1);
    x = Instantiate(wrong, this.transform.position - new Vector3(0, 0, 0.5f), this.transform.rotation) as X_Symbol;
    x.transform.localScale = new Vector3(0, 0, 1);
  }

  public void appearCorrect()
  {
    if (this.correct)
    {
      o.StartCoroutine(o.appear());
    }
    else
    {
      x.StartCoroutine(x.appear());
      //wrong.appear();
    } 
    
  }

  public void disappearCorrect()
  {
    if (correct)
    {
      o.StartCoroutine(o.disappear());
      //right.disappear();
    }
    else
    {
      x.StartCoroutine(x.disappear());
      //wrong.disappear();
    }
  }

    // Start is called before the first frame update
    void Start()
    {
    ani = gameObject.GetComponent<Animator>();
    right.transform.position = this.transform.position;
    wrong.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
