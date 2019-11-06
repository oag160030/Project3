using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerToFollow;   // the player object the camera will follow
    Vector3 cameraOffest;   // offest to save, used for camera position readjustment

    // Start is called before the first frame update
    void Start()
    {
        // calculate offset
        cameraOffest = transform.position - playerToFollow.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // readjust camera position based off of player + offest position
        transform.position = playerToFollow.transform.position + cameraOffest;
    }
}
