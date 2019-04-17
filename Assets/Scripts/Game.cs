using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public GameObject[] tetrominos;

  public static int gridWidth = 10;
  public static int gridHeight = 24;

  public static bool[,] grid = new bool[gridWidth, gridHeight];

  void Start() {
    SpawnTetromino();

  }

  public void UpdateGrid(int x, int y) {
    grid[x,y] = true;
  }

  public bool IsInsideGrid(Vector2 pos) {
    int x = (int)Mathf.Round(pos.x);
    int y = (int)Mathf.Round(pos.y);
    if(!(x >= 0 && x < gridWidth && y >= 0)) {
      return false;
    }
    return !grid[x,y];
  }

  public void SpawnTetromino() {
    GameObject tetromino = tetrominos[Random.Range(0, tetrominos.Length)];
    Instantiate(tetromino, new Vector3(4, 21, 0), Quaternion.identity);
  }
}
