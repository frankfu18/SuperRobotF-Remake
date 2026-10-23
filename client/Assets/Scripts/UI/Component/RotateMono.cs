using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMono : MonoBehaviour
{
    public Vector3 rotateSpeed;
    public Space rotateMode = Space.Self;

    
    void Update()
    {
        this.transform.Rotate(rotateSpeed * Time.unscaledDeltaTime, rotateMode);
    }
}
