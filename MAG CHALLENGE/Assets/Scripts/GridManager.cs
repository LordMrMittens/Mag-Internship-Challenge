using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tiles = new List<GameObject>();
    GameObject[,] grid;
    int rows = 9, columns = 9;
    
    List<GameObject> matchingTiles = new List<GameObject>();
    List<GameObject> checkedTiles = new List<GameObject>();
    
    void Start()
    {
        grid = new GameObject[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                InstatiateTile(i, j);
            }
        }
    }

    private void InstatiateTile(int i, int j)
    {
        grid[i, j] = Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(i - (columns / 2), j - (rows / 2), 0), Quaternion.identity);
    }

    private void Update()
    {
        if (GameManager._gmInstance.timeLeft > 0&& !GameManager._gmInstance.paused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ClickedTile() != null)
                {
                    CheckTile(ClickedTile());
                }
            }
        }
        LookForEmptyTiles();
    } 
    GameObject ClickedTile()
    {
        GameObject clickedTile;
        Collider2D collider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (collider != null)
        {
            clickedTile = collider.gameObject;
        }
        else
        {
            clickedTile = null;
        }
        return clickedTile;
    }
    public void CheckTile(GameObject go)
    {
        CheckForNeighbours(go);
        if (matchingTiles.Count >0 && !matchingTiles.Contains(go))
        {
            matchingTiles.Add(go);
            CheckList(go);
        }
        
        
    }
    public void CheckList(GameObject activeTile)
    {
        CheckForNeighbours(activeTile);
        if (!checkedTiles.Contains(activeTile))
        {
            checkedTiles.Add(activeTile);
            matchingTiles.Remove(activeTile);
        }
        if (matchingTiles.Count > 0)
        {
            CheckList(matchingTiles[0]);
        }
        else
        {
            foreach (GameObject checkedTile in checkedTiles)
            {
                GameManager._gmInstance.addPoints();
                Destroy(checkedTile);
            }
            checkedTiles.Clear();
        }
    }

    private void CheckForNeighbours(GameObject activeTile)
    {
        CheckNearbyTiles(activeTile, Vector2.up);
        CheckNearbyTiles(activeTile, Vector2.down);
        CheckNearbyTiles(activeTile, Vector2.left);
        CheckNearbyTiles(activeTile, Vector2.right);
    }

    private void CheckNearbyTiles(GameObject activeTile, Vector2 directionToCheck)
    {
        RaycastHit2D hit = Physics2D.Raycast(activeTile.transform.position, directionToCheck, 0.5f);
        if (hit.collider != null && hit.transform.gameObject.tag == activeTile.tag)
        {
            if (!matchingTiles.Contains(hit.transform.gameObject) && !checkedTiles.Contains(hit.transform.gameObject))
            {
                matchingTiles.Add(hit.transform.gameObject);
            }
        }
    }
    void LookForEmptyTiles()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (grid[x,y] == null)
                {
                    bool needsNewTile = true;
                    for (int i = y; i < rows; i++)
                    {
                        if (grid[x, i] != null)
                        {
                            needsNewTile = false;
                            ShiftTiles(x, y, i);
                            break;
                        }
                    }
                    SpawnNewTiles(x, needsNewTile);
                    break;
                }

            }
        }
    }

    private void ShiftTiles(int x, int y, int i)
    {
        grid[x,i].GetComponent<TileGravityManager>().ShiftTile(grid[x, i].transform.position, new Vector3(x - (columns / 2), y - (rows / 2), 0));
        grid[x, y] = grid[x, i];
        grid[x, i] = null;
    }
    private void SpawnNewTiles(int x, bool needsNewTile)
    {
        if (needsNewTile)
        {
            GameObject replaceMentTile = Instantiate(tiles[Random.Range(0, tiles.Count)], new Vector3(x - (columns / 2), 10 - (rows / 2), 0), Quaternion.identity);
            replaceMentTile.GetComponent<TileGravityManager>().ShiftTile(replaceMentTile.transform.position, new Vector3(x - (columns / 2), 8 - (rows / 2), 0));
            grid[x, 8] = replaceMentTile;
        }
    }
}
