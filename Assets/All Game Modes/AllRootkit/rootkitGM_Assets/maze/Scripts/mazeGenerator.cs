using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject startPointPrefab;
    public GameObject endPointPrefab;
    public Transform mazeParent;

    private Cell[,] maze;

    private struct Cell
    {
        public bool visited;
        public bool[] walls; // 0=Top, 1=Right, 2=Bottom, 3=Left
    }

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // Create single large floor
        Vector3 center = new Vector3(width / 2f - 0.5f, 0, height / 2f - 0.5f);
        GameObject floor = Instantiate(floorPrefab, center, Quaternion.identity, mazeParent);
        floor.transform.localScale = new Vector3(width, 1f, height);

        // Initialize maze grid
        maze = new Cell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y].walls = new bool[] { true, true, true, true };
            }
        }

        // Carve paths recursively
        CarvePath(0, 0);

        // Build walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 basePos = new Vector3(x, 0.5f, y);

                if (maze[x, y].walls[0]) // Top
                    Instantiate(wallPrefab, basePos + new Vector3(0, 0, 0.5f), Quaternion.identity, mazeParent);

                if (maze[x, y].walls[1]) // Right
                    Instantiate(wallPrefab, basePos + new Vector3(0.5f, 0, 0), Quaternion.Euler(0, 90, 0), mazeParent);

                if (maze[x, y].walls[2]) // Bottom
                    Instantiate(wallPrefab, basePos + new Vector3(0, 0, -0.5f), Quaternion.identity, mazeParent);

                if (maze[x, y].walls[3]) // Left
                    Instantiate(wallPrefab, basePos + new Vector3(-0.5f, 0, 0), Quaternion.Euler(0, 90, 0), mazeParent);
            }
        }

        // Spawn Start Point at (0,0)
        Vector3 startPos = new Vector3(0, 0.5f, 0);
        Instantiate(startPointPrefab, startPos, Quaternion.identity, mazeParent);

        // Spawn End Point at a random cell (not (0,0))
        List<Vector2Int> validCells = new List<Vector2Int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!(x == 0 && y == 0))
                    validCells.Add(new Vector2Int(x, y));
            }
        }

        Vector2Int endCoord = validCells[Random.Range(0, validCells.Count)];
        Vector3 endPos = new Vector3(endCoord.x, 0.5f, endCoord.y);
        Instantiate(endPointPrefab, endPos, Quaternion.identity, mazeParent);
    }

    void CarvePath(int x, int y)
    {
        maze[x, y].visited = true;

        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1),  // Up
            new Vector2Int(1, 0),  // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0)  // Left
        };
        Shuffle(directions);

        foreach (var dir in directions)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            if (IsInside(nx, ny) && !maze[nx, ny].visited)
            {
                int currentWall = GetWallIndex(dir);
                int neighborWall = GetWallIndex(-dir);

                maze[x, y].walls[currentWall] = false;
                maze[nx, ny].walls[neighborWall] = false;

                CarvePath(nx, ny);
            }
        }
    }

    int GetWallIndex(Vector2Int dir)
    {
        if (dir == Vector2Int.up) return 0;
        if (dir == Vector2Int.right) return 1;
        if (dir == Vector2Int.down) return 2;
        return 3; // left
    }

    bool IsInside(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    void Shuffle(List<Vector2Int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            Vector2Int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}
