using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  public float delay = 1f;

  public float[] rotations;
  private int rotation = 0;

  private float fall;
  private Game gameScript;
  private bool placed = false;

  void Start() {
    gameScript = FindObjectOfType<Game>();
  }

  void Update() {
    if(placed) {
      return;
    }
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
    } else if(Input.GetKeyDown(KeyCode.DownArrow) || Time.time > fall + delay) {
      transform.position += new Vector3(0, -1, 0);
      fall = Time.time;
      if(!IsValid()) {
        transform.position += new Vector3(0, 1, 0);
        foreach(Transform mino in transform) {
          gameScript.UpdateGrid((int)mino.position.x, (int)mino.position.y);
        }
        placed = true;
        gameScript.SpawnTetromino();
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
