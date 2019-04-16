using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public static int gridWidth = 10;
  public static int gridHeight = 20;

  void Start() {
      
  }

  void Update() {
      
  }

  public bool IsInsideGrid(Vector2 pos) {
    return ((int)pos.x >= 0 && (int)pos.x < gridWidth & (int)pos.y >= 0);
  }
}
