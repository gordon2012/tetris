using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  public float delay = 1.0f;

  private float fall;
  private Game gameScript;

  void Start() {
    gameScript = FindObjectOfType<Game>();
  }

  void Update() {
    if(Input.GetKeyDown(KeyCode.RightArrow)) {
      transform.position += new Vector3(1, 0, 0);
      if(!IsValid()) {
        transform.position += new Vector3(-1, 0, 0);
      }
    } else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
      transform.position += new Vector3(-1, 0, 0);
      if(!IsValid()) {
        transform.position += new Vector3(1, 0, 0);
      }
    } else if(Input.GetKeyDown(KeyCode.UpArrow)) {
      transform.Rotate(0, 0, 90);
      if(!IsValid()) {
        transform.Rotate(0, 0, -90);
      }
    } else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time > fall + delay) {
      transform.position += new Vector3(0, -1, 0);
      fall = Time.time;
      if(!IsValid()) {
        transform.position += new Vector3(0, 1, 0);
      }
    }      
  }

  bool IsValid() {
    foreach(Transform mino in transform) {
      if(!gameScript.IsInsideGrid(mino.position)) {
        return false;
      }
    }
    return true;
  }
}
