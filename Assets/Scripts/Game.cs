using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public GameObject[] tetrominos;

  public static int gridWidth = 10;
  public static int gridHeight = 20;

  void Start() {
    SpawnTetromino();
  }

  void Update() {
      
  }

  public bool IsInsideGrid(Vector2 pos) {
    return ((int)pos.x >= 0 && (int)pos.x < gridWidth & (int)pos.y >= 0);
  }

  public void SpawnTetromino() {
    GameObject tetromino = tetrominos[Random.Range(0, tetrominos.Length)];
    Instantiate(tetromino, new Vector3(4, 21, 0), Quaternion.identity);
  }
}
