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

  public static Transform[,] grid = new Transform[gridWidth, gridHeight];

  void Start() {
    SpawnTetromino();
  }

  public void UpdateGrid(int x, int y, Transform mino) {
    // xxx: x, y

    Transform newMino = Instantiate(mino, new Vector3(x, y, 0), mino.transform.rotation);
    grid[x,y] = newMino;

    // if(debug) {
    //   Instantiate(gridDot, new Vector3(x, y, 0), Quaternion.identity);
    // } else {
    //   // Debug.Log(mino.transform.rotation);
    // }


    // int

    GameObject[] dots = GameObject.FindGameObjectsWithTag("Debug");
    foreach(GameObject dot in dots) {
      GameObject.Destroy(dot);
    }

    // int minoCount = 0;
    for(int i = gridHeight - 1; i >= 0; i--) {
      int rowCount = 0;
      for(int j = 0; j < gridWidth; j++) {
        // Debug.Log(grid[j,i] != null);
        if(grid[j,i] != null) {
          Instantiate(gridDot, new Vector3(j, i, 0), Quaternion.identity);
          rowCount++;
          // minoCount++;
        }
      }
      if(rowCount >= 10) {
        // Debug.Log("Row: " + i + ", count: " + rowCount);
        Instantiate(gridDot, new Vector3(-2, i, 0), Quaternion.identity);



      }
    }

    DestroyRow(0);

  }

  public void CheckRows() {
    for(int i = gridHeight - 1; i >= 0; i--) {
      int rowCount = 0;
      for(int j = 0; j < gridWidth; j++) {
        if(grid[j,i] != null) {
          rowCount++;
        }
      }
      if(rowCount >= 10) {

        // destroy this row, move others down
        DestroyRow(i);

      }
    }
  }

  public void DestroyRow(int y) {

    for(int x = 0; x < gridWidth; x++) {
      if(grid[x,y] != null) {
        DestroyImmediate(grid[x,y].gameObject, true);
        grid[x,y] = null;
      }
    }


    for(int h = y; h < gridHeight; h++) {
      // Instantiate(gridDot, new Vector3(-3, h, 0), Quaternion.identity);


        if(h == gridHeight - 1) {
          Debug.Log("TOP");
        }

      for(int w = 0; w < gridWidth; w++) {




      }


    }

  }


  public bool IsInsideGrid(Vector2 pos) {
    int x = (int)Mathf.Round(pos.x);
    int y = (int)Mathf.Round(pos.y);
    if(!(x >= 0 && x < gridWidth && y >= 0)) {
      return false;
    }
    // Debug.Log(x + ", " + y + ": " + grid[x,y]);
    return grid[x,y] == null;
  }

  public void SpawnTetromino() {
    GameObject tetromino = tetrominos[Random.Range(0, tetrominos.Length)];
    Instantiate(tetromino, new Vector3(4, 21, 0), Quaternion.identity);
  }
}
