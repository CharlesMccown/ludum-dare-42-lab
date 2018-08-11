using System;
using UnityEngine;

internal class InputHandler : MonoBehaviour 
{
    internal Action<Vector3> OnMotion;
    
    private void Update()
    {
        var motionVector = Vector3.zero;
        if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0)
            motionVector.x = 1;
        else if (Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0)
            motionVector.x = -1;

        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            motionVector.y = 1;
        else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
            motionVector.y = -1;

        if (motionVector != Vector3.zero && OnMotion != null)
            OnMotion(motionVector);
    }
}
