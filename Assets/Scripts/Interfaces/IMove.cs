using System;
using UnityEngine;

public interface IMove
{
    float Speed { get; }
    Action<Vector3, float> OnMoved { get; set; }
}
