using System;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
internal class Movement : MonoBehaviour, IMove
{
    [SerializeField]
    private float speed = 0.2f;
    public int Moves { get; private set; }

    public float Speed {
        get { return speed; }
    }

    public Action OnWalk;
    public Action<Vector3, float> OnMoved { get; set; }

    private InputHandler inputHandler;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        inputHandler.OnMotion += HandleMotion;
    }

    private void HandleMotion(Vector3 motionVector)
    {
        transform.position += motionVector * Speed;
        Moves++;
        OnWalk?.Invoke();
        OnMoved?.Invoke(transform.position, speed);
    }
}
