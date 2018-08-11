using UnityEngine;

internal class MazeData
{
    internal float complexity;

    internal MazeData(float complexity)
    {
        this.complexity = complexity;
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

        for (int rowIndex = 0; rowIndex <= maxRows; rowIndex++)
        {
            GenerateMazeRow(maze, maxRows, maxColumns, exitColumIndex, exitRowIndex, rowIndex);
        }
        return maze;
    }
    
    private void GenerateMazeRow(int[,] maze, int maxRows, int maxColumns, int exitColumIndex, int exitRowIndex, int rowIndex)
    {
        for (int columnIndex = 0; columnIndex <= maxColumns; columnIndex++)
        {
            GenerateMazeColumn(maze, maxRows, maxColumns, exitColumIndex, exitRowIndex, rowIndex, columnIndex);
        }
    }

    private void GenerateMazeColumn(int[,] maze, int maxRows, int maxColumns, int exitColumIndex, int exitRowIndex, int rowIndex, int columnIndex)
    {
        if (IsExitCell(rowIndex, columnIndex, exitColumIndex, exitRowIndex))
        {
            maze[rowIndex, columnIndex] = 2;
        }
        else if (IsEdgeCell(rowIndex, columnIndex, maxRows, maxColumns))
        {
            maze[rowIndex, columnIndex] = 1;
        }
        else if (rowIndex % 2 == 0 && columnIndex % 2 == 0)
        {
            GenerateMazeCell(maze, rowIndex, columnIndex, exitRowIndex, exitColumIndex);
        }
    }

    private bool IsEdgeCell(int rowIndex, int columnIndex, int maxRows, int maxColumns)
    {
        return rowIndex == 0 || columnIndex == 0 || rowIndex == maxRows || columnIndex == maxColumns;
    }

    private bool IsExitCell(int rowIndex, int columnIndex, int exitColumIndex, int exitRowIndex)
    {
        return rowIndex == exitRowIndex && columnIndex == exitColumIndex;
    }

    private void GenerateMazeCell(int[,] maze, int rowIndex, int columnIndex, int exitRowIndex, int exitColumnIndex)
    {
        if (!IsAdjacentExit(rowIndex, columnIndex, exitRowIndex, exitColumnIndex) && Random.value > complexity)
        {
            maze[rowIndex, columnIndex] = 1;

            int randomRowOffset = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
            int randomColumnOffset = randomRowOffset != 0 ? 0 : (Random.value < .5 ? -1 : 1);
            maze[rowIndex + randomRowOffset, columnIndex + randomColumnOffset] = 1;
        }
    }

    private bool IsAdjacentExit(int rowIndex, int columnIndex, int exitRowIndex, int exitColumnIndex)
    {
        return rowIndex + 1 == exitRowIndex && (columnIndex + 1 == exitColumnIndex || columnIndex - 1 == exitColumnIndex) ||
            rowIndex - 1 == exitRowIndex && (columnIndex + 1 == exitColumnIndex || exitColumnIndex - 1 == exitColumnIndex);
    }
}
