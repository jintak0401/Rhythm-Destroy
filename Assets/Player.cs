using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Moving
{
  public Sprite[] movingSprites;
}

[System.Serializable]
public class Attacking
{
  public Sprite[] attackingSprites;
}

public class Player : MonoBehaviour
{

  public bool isMe;
  private int who;
  public Moving[] moving;
  public Attacking[] attacking;
  private SpriteRenderer spriteRenderer;

  public IEnumerator move()
  {
    int who = (isMe) ? 1 : 0;
    for (int i = 0; i < 100; i++)
    {
      spriteRenderer.sprite = moving[who].movingSprites[i % moving[0].movingSprites.Length];
      yield return new WaitForSeconds(0.8f);
      if (i % 4 == 3)
      {
        yield return new WaitForSeconds(1f);
      }
    }
  }

  public IEnumerator attack()
  {
    for (int i = 0; i < 100; i++)
    {
      spriteRenderer.sprite = attacking[who].attackingSprites[i % attacking[0].attackingSprites.Length];
      yield return new WaitForSeconds(0.1f);
      if (i % 4 == 3)
      {
        yield return new WaitForSeconds(0.3f);
      }
    }
  }

  // Start is called before the first frame update
  void Start()
    {
    spriteRenderer = GetComponent<SpriteRenderer>();
    who = isMe ? 0 : 1;
    StartCoroutine(attack());
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
