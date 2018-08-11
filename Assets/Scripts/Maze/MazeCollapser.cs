using UnityEngine;

[RequireComponent(typeof(IMove))]
internal class MazeCollapser :MonoBehaviour
{
    [SerializeField]
    private GameObject collapsedWallPrefab;
    [SerializeField]
    private float instability = 0.1f;
    [SerializeField]
    private int limitDistance = 1;
    [SerializeField]
    private int maxDistance = 10;
    private IMove move;

    internal void Awake()
    {
        move = GetComponent<IMove>();
        move.OnMoved += HandleMove;
    }

    private void HandleMove(Vector3 position, float speed)
    {
        if (Random.value > instability)
        {
            int horizontalDistance = (int)(Random.Range(-maxDistance, maxDistance));
            horizontalDistance = limitToMinimumDistance(horizontalDistance);
            int verticalDistance = (int)(Random.Range(-maxDistance, maxDistance));
            verticalDistance = limitToMinimumDistance(verticalDistance);

            Instantiate(collapsedWallPrefab, position + (new Vector3(horizontalDistance, verticalDistance, 0) * speed), transform.rotation);
        }
    }

    private int limitToMinimumDistance(int distance)
    {
        if (distance > 0)
            return (int)Mathf.Max(limitDistance, distance);
        else
            return (int)Mathf.Min(-limitDistance, distance);
    }
}