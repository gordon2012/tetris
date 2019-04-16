using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  public BoxCollider2D bc;
  public float delay = 1.0f;

  private float fall;

  void Start()
  {
    bc = GetComponent<BoxCollider2D>();
    // InvokeRepeating("Fall", 1.0f, 0.2f);
  }

  void Update()
  {
    // User input

    if(Input.GetKeyDown(KeyCode.RightArrow)) {
      transform.position += new Vector3(1, 0, 0);
    } else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
      transform.position += new Vector3(-1, 0, 0);
    } else if(Input.GetKeyDown(KeyCode.UpArrow)) {
      transform.Rotate(0, 0, 90);
    } else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time > fall + delay) {
      transform.position += new Vector3(0, -1, 0);
      fall = Time.time;
    }
      
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
