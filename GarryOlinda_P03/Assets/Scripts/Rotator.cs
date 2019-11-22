using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 10f;

    [SerializeField] Vector3 rotateDirection = new Vector3(0, 1, 0);

    private void Update()
    {
        // rotate the object
        transform.Rotate(rotateDirection * rotateSpeed);
    }
}
