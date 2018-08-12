using UnityEngine;

[RequireComponent(typeof(IMove))]
internal class MazeCollapser :MonoBehaviour
{
    [SerializeField]
    private GameObject collapsedWallPrefab;
    [SerializeField]
    private DifficultySettings settings;
    private IMove move;

    internal void Awake()
    {
        move = GetComponent<IMove>();
        move.OnMoved += HandleMove;
    }

    private void HandleMove(Vector3 position, float speed)
    {
        if (Random.value > settings.CurrentDifficulty.Instability)
        {
            int horizontalDistance = (int)(Random.Range(-settings.CurrentDifficulty.MaxDistance, settings.CurrentDifficulty.MaxDistance));
            horizontalDistance = limitToMinimumDistance(horizontalDistance);
            int verticalDistance = (int)(Random.Range(-settings.CurrentDifficulty.MaxDistance, settings.CurrentDifficulty.MaxDistance));
            verticalDistance = limitToMinimumDistance(verticalDistance);

            Instantiate(collapsedWallPrefab, position + (new Vector3(horizontalDistance, verticalDistance, 0) * speed), transform.rotation);
        }
    }

    private int limitToMinimumDistance(int distance)
    {
        if (distance > 0)
            return (int)Mathf.Max(settings.CurrentDifficulty.LimitDistance, distance);
        else
            return (int)Mathf.Min(-settings.CurrentDifficulty.LimitDistance, distance);
    }
}