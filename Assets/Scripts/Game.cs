using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
  public GameObject[] tetrominos;
  public GameObject gridDot;
  public bool debug = false;

  public static int gridWidth = 10;
  public static int gridHeight = 24;

  public static GameObject[,] grid = new GameObject[gridWidth, gridHeight];

  void Start() {
    SpawnTetromino();
  }

  public void UpdateGrid(int x, int y, GameObject mino) {
    grid[x,y] = mino;
    if(debug) {
      Instantiate(gridDot, new Vector3(x, y, 0), Quaternion.identity);
    } else {
      // Debug.Log(mino.transform.rotation);
    }
    Instantiate(mino, new Vector3(x, y, 0), mino.transform.rotation);

    // int

    GameObject[] dots = GameObject.FindGameObjectsWithTag("Debug");
    foreach(GameObject dot in dots) {
      GameObject.Destroy(dot);
    }


    for(int i = gridHeight - 1; i >= 0; i--) {
      int rowCount = 0;
      for(int j = 0; j < gridWidth; j++) {
        Debug.Log(grid[j,i]);
        if(grid[j,i] != null) {
          Instantiate(gridDot, new Vector3(j, i, 0), Quaternion.identity);
          rowCount++;
        }
      }
      if(rowCount >= 10) {
        Debug.Log("Row: " + i + ", count: " + rowCount);
        Instantiate(gridDot, new Vector3(-2, i, 0), Quaternion.identity);
      }
    }

  }

  public bool IsInsideGrid(Vector2 pos) {
    int x = (int)Mathf.Round(pos.x);
    int y = (int)Mathf.Round(pos.y);
    if(!(x >= 0 && x < gridWidth && y >= 0)) {
      return false;
    }
    return grid[x,y] == null;
  }

  public void SpawnTetromino() {
    GameObject tetromino = tetrominos[Random.Range(0, tetrominos.Length)];
    Instantiate(tetromino, new Vector3(4, 21, 0), Quaternion.identity);
  }
}
