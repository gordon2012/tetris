using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
  public GameObject[] tetrominos;
  public GameObject gridDot;
  public bool debug = false;

  public static int gridWidth = 10;
  public static int gridHeight = 20;

  public static Transform[,] grid = new Transform[gridWidth, gridHeight];

  public int[] scoreValues = {0, 40, 100, 300, 1200};
  private int score = 0;
  public Text hudScore;

  public int level = 0;
  private int rows = 0;
  public Text hudLevel;

  private int lastSpawn = -1;

  public Transform previewPoint;
  private GameObject previewTetromino;
  private int nextSpawn = -1;

  void Start() {
    PreviewTetromino();
    SpawnTetromino();
    hudLevel.text = level.ToString();
  }

  public void PlaceTetromino(Transform tetromino) {
    foreach(Transform mino in tetromino) {
      int x = (int)Mathf.Round(mino.transform.position.x);
      int y = (int)Mathf.Round(mino.transform.position.y);
      grid[x,y] = Instantiate(mino, new Vector3(x, y, 0), mino.transform.rotation);
    }
    CheckRows();
  }

  public void CheckRows() {
    int numDestroyed = 0;
    for(int i = gridHeight - 1; i >= 0; i--) {
      int rowCount = 0;
      for(int j = 0; j < gridWidth; j++) {
        if(grid[j,i] != null) {
          rowCount++;
        }
      }
      if(rowCount >= 10) {
        DestroyRow(i);
        numDestroyed++;
      }
    }
    IncreaseScore(numDestroyed);

    if(debug) {
      GameObject[] dots = GameObject.FindGameObjectsWithTag("Debug");
      foreach(GameObject dot in dots) {
        GameObject.Destroy(dot);
      }
      for(int h = 0; h < gridHeight; h++) {
        for(int w = 0; w < gridWidth; w++) {
          if(grid[w,h] != null) {
            Instantiate(gridDot, new Vector3(w, h, 0), Quaternion.identity);
          }
        }
      }
    }
  }

  public void DestroyRow(int y) {
    for(int x = 0; x < gridWidth; x++) {
      if(grid[x,y] != null) {
        DestroyImmediate(grid[x,y].gameObject, true);
      }
    }
    for(int h = y; h < gridHeight; h++) {
      if(h == gridHeight - 1) {
        for(int w = 0; w < gridWidth; w++) {
          grid[w,h] = null;
        }
      } else {
        for(int w = 0; w < gridWidth; w++) {
          grid[w,h] = grid[w,h+1];
          if(grid[w,h] != null) {
            grid[w,h].position += new Vector3(0, -1, 0);
          }
        }
      }
    }
  }

  public bool IsInsideGrid(Vector2 pos) {
    int x = (int)Mathf.Round(pos.x);
    int y = (int)Mathf.Round(pos.y);
    if(!(x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)) {
      return false;
    }
    return grid[x,y] == null;
  }

  public void PreviewTetromino() {
    int random = Random.Range(0, tetrominos.Length + 1);
    if(random == tetrominos.Length || random == lastSpawn) {
      random = Random.Range(0, tetrominos.Length);
    }
    lastSpawn = nextSpawn;
    nextSpawn = random;

    Destroy(previewTetromino);
    previewTetromino = Instantiate(tetrominos[random], previewPoint.position, Quaternion.identity);
  }

  public void SpawnTetromino() {
    GameObject spawnedTetromino = Instantiate(tetrominos[nextSpawn], new Vector3(4, 19, 0), Quaternion.identity);
    spawnedTetromino.GetComponent<Block>().Init();
    PreviewTetromino();
  }

  void IncreaseScore(int rowsDestroyed) {
    if(scoreValues[rowsDestroyed] > 0) {
      score += scoreValues[rowsDestroyed];
      hudScore.text = score.ToString();
    }
    rows += rowsDestroyed;
    if(rows >= (level + 1) * 10) {
      level++;
      hudLevel.text = level.ToString();
    }
  }
}
