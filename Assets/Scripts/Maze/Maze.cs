using UnityEngine;

[RequireComponent(typeof(MazeGenerator))]
public class Maze : MonoBehaviour 
{
    private MazeGenerator generator;
    [SerializeField]
    private int width = 13;
    [SerializeField]
    private int height = 15;

    void Start()
    {        
        generator = GetComponent<MazeGenerator>();
        generator.GenerateNewMaze(width, height);
    }
}
