using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  private Game gameScript;

  public float[] rotations;
  private int rotation = 0;

  private float delay = 1f;
  private float fall;

  private bool isPushing = false;
  private float pushDelay = 0.3f;
  private float pushSpeed = 0.03f;
  private float push;

  void Start() {
    gameScript = FindObjectOfType<Game>();
    fall = Time.time;
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
    } else if(Input.GetKeyDown(KeyCode.UpArrow) && rotations.Length > 0) {
      int oldRotation = rotation;
      rotation = (rotation + 1) % rotations.Length;
      transform.rotation = Quaternion.Euler(0, 0, rotations[rotation]);
      if(!IsValid()) {
        transform.rotation = Quaternion.Euler(0, 0, rotations[oldRotation]);
        rotation = oldRotation;
      }
    } else {
      bool isDown = false;

      if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time > fall + delay) {
        isDown = true;
      } else if(isPushing && Time.time > push + pushSpeed) {
        isDown = true;
      } else if(Input.GetKey(KeyCode.DownArrow) && Time.time > push + pushDelay) {
        isPushing = true;
      }
      if(Input.GetKeyUp(KeyCode.DownArrow)) {
        isPushing = false;
      }

      if(isDown) {
        transform.position += new Vector3(0, -1, 0);
        push = Time.time;
        fall = Time.time;
        if(!IsValid()) {
          transform.position += new Vector3(0, 1, 0);
          gameScript.PlaceTetromino(transform);
          gameScript.SpawnTetromino();
          DestroyImmediate(this.gameObject, true);
        }
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
