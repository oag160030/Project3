using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField] float speed = 50;

    [SerializeField] Transform startLocation = null;
    [SerializeField] Transform targetLocation = null;
    [SerializeField] Transform targetLocationHome = null;

    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = startLocation.position;
            targetLocation.position = targetLocationHome.position;
            StartCoroutine(ThrowHatRoutine(1f));
        }
    }

    IEnumerator ThrowHatRoutine(float timeToWait)
    {
        yield return HatMoveRoutine(startLocation.position, targetLocation.position, 2f);
        yield return new WaitForSeconds(timeToWait);
        yield return HatMoveRoutine(targetLocation.position, startLocation.position, 2f);
    }


    IEnumerator HatMoveRoutine(Vector3 a, Vector3 b,float timeToWait)
    {
        float i = 0f;
        float rate = (1f / timeToWait) * speed;
        while(i < 1f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
