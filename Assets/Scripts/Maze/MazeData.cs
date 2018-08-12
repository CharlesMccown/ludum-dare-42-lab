using System.Linq;
using UnityEngine;

internal class MazeData
{
    private float complexity;
    private int safePathDistance;

    internal MazeData(float complexity, int safePathDistance)
    {
        this.complexity = complexity;
        this.safePathDistance = safePathDistance;
    }

    internal int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        int maxRows = maze.GetUpperBound(0);
        int maxColumns = maze.GetUpperBound(1);
                
        int exitColumIndex = 0;
        int exitRowIndex = 0;
        switch(Random.Range(0,4))
        {
            case 0:
                exitColumIndex = 0;
                exitRowIndex = Random.Range(1, maxRows -1);
                break;
            case 1:
                exitColumIndex = maxColumns;
                exitRowIndex = Random.Range(1, maxRows -1);
                break;
            case 2:
                exitColumIndex = Random.Range(1, maxColumns -1);
                exitRowIndex = 0;
                break;
            case 3:
                exitColumIndex = Random.Range(1, maxColumns -1);
                exitRowIndex = maxRows;
                break;
            default:
                Debug.Log("Unexpected Random Range in MazeData. What did you do?");
                break;
        }

        Vector2[] safePath = generateSafePath(exitColumIndex, exitRowIndex);

        for (int rowIndex = 0; rowIndex <= maxRows; rowIndex++)
        {
            GenerateMazeRow(maze, safePath, maxRows, maxColumns, exitColumIndex, exitRowIndex, rowIndex);
        }
        return maze;
    }

    private Vector2[] generateSafePath(int exitColumnIndex, int exitRowIndex)
    {
        int columnIndex = exitColumnIndex;
        int rowIndex = exitRowIndex;
        Vector2[] safePath = new Vector2[safePathDistance];
        safePath[0] = new Vector2(columnIndex, rowIndex);
        for (int stepIndex = 1; stepIndex < safePath.Length; stepIndex++)
        {
            safePath[stepIndex] = generateNextStep(safePath[stepIndex-1], columnIndex, rowIndex);
        }
        return safePath;
    }

    private Vector2 generateNextStep(Vector2 lastStep, int currentColumn, int currentRow)
    {
        int columnIndex = currentColumn;
        int rowIndex = currentRow;
        if (Random.value < .5f)
            columnIndex = columnIndex < 0 ? columnIndex++ : columnIndex--;
        else
            rowIndex = rowIndex < 0 ? rowIndex++ : rowIndex--;
        return new Vector2(columnIndex, rowIndex);       
    }

    private void GenerateMazeRow(int[,] maze, Vector2[] safePath, int maxRows, int maxColumns, int exitColumIndex, int exitRowIndex, int rowIndex)
    {
        for (int columnIndex = 0; columnIndex <= maxColumns; columnIndex++)
        {
            GenerateMazeColumn(maze, safePath, maxRows, maxColumns, exitColumIndex, exitRowIndex, rowIndex, columnIndex);
        }
    }

    private void GenerateMazeColumn(int[,] maze, Vector2[] safePath, int maxRows, int maxColumns, int exitColumIndex, int exitRowIndex, int rowIndex, int columnIndex)
    {
        if (IsExitCell(rowIndex, columnIndex, exitColumIndex, exitRowIndex))
        {
            SetCell(maze, columnIndex, rowIndex, 2);
        }
        else if (IsEdgeCell(rowIndex, columnIndex, maxRows, maxColumns))
        {
            SetCell(maze, columnIndex, rowIndex, 1);
        }
        else if(safePath.Contains(new Vector2(columnIndex, rowIndex)))
        {
            SetCell(maze, columnIndex, rowIndex, 0);
        }
        else if (rowIndex % 2 == 0 && columnIndex % 2 == 0)
        {
            GenerateMazeCell(maze, rowIndex, columnIndex, exitRowIndex, exitColumIndex);
        }
    }

    private void GenerateMazeCell(int[,] maze, int rowIndex, int columnIndex, int exitRowIndex, int exitColumnIndex)
    {
        if (!IsAdjacentExit(rowIndex, columnIndex, exitRowIndex, exitColumnIndex) && Random.value > complexity)
        {
            SetCell(maze,columnIndex, rowIndex, 1);

            int randomRowOffset = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
            int randomColumnOffset = randomRowOffset != 0 ? 0 : (Random.value < .5 ? -1 : 1);
            SetCell(maze, columnIndex + randomColumnOffset, rowIndex + randomRowOffset, 1);
        }
    }

    private void SetCell(int[,] maze, int columnIndex, int rowIndex, int value)
    {
        maze[columnIndex, rowIndex] = value;
    }

    private bool IsEdgeCell(int rowIndex, int columnIndex, int maxRows, int maxColumns)
    {
        return rowIndex == 0 || columnIndex == 0 || rowIndex == maxRows || columnIndex == maxColumns;
    }

    private bool IsExitCell(int rowIndex, int columnIndex, int exitColumIndex, int exitRowIndex)
    {
        return rowIndex == exitRowIndex && columnIndex == exitColumIndex;
    }

    private bool IsAdjacentExit(int rowIndex, int columnIndex, int exitRowIndex, int exitColumnIndex)
    {
        return rowIndex + 1 == exitRowIndex && (columnIndex + 1 == exitColumnIndex || columnIndex - 1 == exitColumnIndex) ||
            rowIndex - 1 == exitRowIndex && (columnIndex + 1 == exitColumnIndex || exitColumnIndex - 1 == exitColumnIndex);
    }    
}
