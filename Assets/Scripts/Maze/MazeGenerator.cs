using System;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int[,] Maze { get; private set; }

    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject escapePrefab;
    [SerializeField]
    private DifficultySettings settings;
    private MazeData mazeData;

    public int[,] data
    {
        get; private set;
    }

    void Awake()
    {
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
        mazeData = new MazeData(settings.CurrentDifficulty.Complexity, settings.CurrentDifficulty.SafePathDistance);
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        var offset = new Vector3((sizeCols / 2) * transform.localScale.x, (sizeRows / 2 * transform.localScale.y), 0);

        data = mazeData.FromDimensions(sizeRows, sizeCols);
        buildMaze(data, offset);
    }

    private void buildMaze(int[,] data, Vector3 offset)
    {
        Maze = data;
        int rMax = Maze.GetUpperBound(0);
        int cMax = Maze.GetUpperBound(1);        
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                var wallPosition = new Vector3(i * transform.localScale.x, j * transform.localScale.y, 0) - offset;
                if (wallPosition != Vector3.zero && Maze[i, j] == 1)
                {
                    Instantiate(wallPrefab, wallPosition, transform.rotation);
                }
                else if(Maze[i, j] == 2)
                {
                    Instantiate(escapePrefab, wallPosition, transform.rotation);
                }
            }
        }
    }
}
