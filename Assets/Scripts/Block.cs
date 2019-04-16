using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  public BoxCollider2D bc;

  void Start()
  {
    bc = GetComponent<BoxCollider2D>();
    InvokeRepeating("Fall", 1.0f, 0.2f);
  }

  void Update()
  {
      
  }

  void Fall() {
    if(transform.localPosition.y < -17) {
      Debug.Log("PLACED");
      CancelInvoke();
      return;
    }

    transform.Translate(0, -1f, 0);
    Debug.Log(transform.localPosition.y);
  }
}
