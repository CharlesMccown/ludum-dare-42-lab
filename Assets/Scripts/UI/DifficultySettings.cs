using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty", menuName = "/Settings/Difficulty")]
public class DifficultySettings : ScriptableObject
{
    [SerializeField]
    private int difficulty = 0;

    public void AdjustDifficulty()
    {
        difficulty = (difficulty+1) % 5;
    }

    internal void ResetDifficulty()
    {
        difficulty = 0;
    }

    public Difficulty CurrentDifficulty
    {
        get
        {
            return GetCurrentDifficulty();
        }
    }

    private Difficulty GetCurrentDifficulty()
    {
        if(difficulty <0) return new Difficulty(0.9f, 0.9f, 10, 5, 20, "Too Easy");
        switch (difficulty)
        {
            case 0:
                return new Difficulty(0.7f, 0.7f, 7, 3, 20, "Beginner");
            case 1:
                return new Difficulty(0.6f, 0.6f, 5, 3, 15, "Novice");
            case 2:
                return new Difficulty(0.5f, 0.5f, 4, 2, 12, "Normal");
            case 3:
                return new Difficulty(0.4f, 0.3f, 4, 2, 10, "Hard");
            case 4:
                return new Difficulty(0.2f, 0.1f, 3, 2, 8, "Improbable");
            default:
                return new Difficulty(0.05f, 0.0f, 2, 1, 50, "Impossible");
        }
    }
}
