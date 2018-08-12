using TMPro;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField]
    private DifficultySettings settings;
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        settings.ResetDifficulty();
    }

    public void AdjustDifficulty()
    {
        settings.AdjustDifficulty();
        textMeshPro.text = settings.CurrentDifficulty.Name;
    }
}
