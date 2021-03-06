﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
  private Game gameScript;

  public float[] rotations;
  private int rotation = 0;

  private float delay;
  private float fall;

  private bool isPushing = false;
  private float pushDelay = 0.3f;
  private float pushSpeed = 0.03f;
  private float push;

  private bool hMove = false;
  private bool hDelay = false;
  private float hTime;

  public bool active = false;

  public void Init() {
    active = true;
  }

  void Start() {
    gameScript = FindObjectOfType<Game>();
    push = Time.time;
    fall = Time.time;
    if(gameScript.level >= 25) {
      delay = 0.02f;
    } else if(gameScript.level >= 9) {
      delay = 0.1f - (gameScript.level - 9) * 0.005f;
    } else {
      delay = 0.8f - gameScript.level * 0.08f;
    }

    if(active && !IsValid()) {
      SceneManager.LoadScene("GameOver");
    }
  }

  void Move(float dir) {
    if(dir == -1) {
      transform.position += new Vector3(-1, 0, 0);
      if(!IsValid()) {
        transform.position += new Vector3(1, 0, 0);
      }
    } else if(dir == 1) {
      transform.position += new Vector3(1, 0, 0);
      if(!IsValid()) {
        transform.position += new Vector3(-1, 0, 0);
      }
    }
  }

  void Update() {
    if(!active) {
      return;
    }
    float hDir = Input.GetAxisRaw("Horizontal");

    if(!hMove && hDir != 0) {
      Move(hDir);
      hMove = true;
      hTime = Time.time;
    }

    if(hMove && !hDelay && Time.time > hTime + pushDelay) {
      hDelay = true;
    }

    if(hMove && hDelay && Time.time > hTime + pushSpeed) {
      Move(hDir);
      hTime = Time.time;
    }

    if(hMove && hDir == 0) {
      hMove = false;
      hDelay = false;
    }

    if(Input.GetKeyDown(KeyCode.UpArrow) && rotations.Length > 0) {
      int oldRotation = rotation;
      rotation = (rotation + 1) % rotations.Length;
      transform.rotation = Quaternion.Euler(0, 0, rotations[rotation]);
      if(!IsValid()) {
        transform.position += new Vector3(1, 0, 0);
        if(!IsValid()) {
          transform.position += new Vector3(-2, 0, 0);
          if(!IsValid()) {
            transform.position += new Vector3(1, -1, 0);
            if(!IsValid()) {
              transform.position += new Vector3(0, 1, 0);
              transform.rotation = Quaternion.Euler(0, 0, rotations[oldRotation]);
              rotation = oldRotation;
            }
          }
        }
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
