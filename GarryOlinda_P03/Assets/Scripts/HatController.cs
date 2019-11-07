using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField] GameObject hat = null;
    [SerializeField] float speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hat.transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
