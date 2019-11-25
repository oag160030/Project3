using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField] float speed = 50;
    [SerializeField] float hatThrowTime = 2f;
    [SerializeField] float spinInPlaceTime = 1f;

    [SerializeField] Rotator hatObject = null;
    [SerializeField] TrailRenderer trail1, trail2; 

    [SerializeField] Transform startLocation = null;
    [SerializeField] Transform targetLocation = null;
    [SerializeField] Transform targetLocationHome = null;
    [SerializeField] Transform returnLocation = null;

    [SerializeField] AudioSource hatAudio;

    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        hatObject.GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            hatObject.GetComponent<MeshRenderer>().enabled = false;
            transform.position = startLocation.position;
            targetLocation.position = targetLocationHome.position;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(ThrowHatRoutine(hatThrowTime, spinInPlaceTime));
            
        }
    }

    IEnumerator WaitForThrowCoroutine()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ThrowHatRoutine(float travelTime, float pauseTime)
    {
        yield return new WaitForSeconds(1.1f);
        yield return hatObject.GetComponent<MeshRenderer>().enabled = true;
        yield return trail1.emitting = true;
        yield return trail2.emitting = true;
        hatAudio.Play(0);

        yield return HatMoveRoutine(startLocation.position, targetLocation.position, travelTime);

        yield return gameObject.GetComponent<BoxCollider>().enabled = true;
        
        yield return new WaitForSeconds(pauseTime);

        
        yield return gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return HatMoveRoutine(targetLocation.position, returnLocation.position, travelTime);

        yield return hatObject.GetComponent<MeshRenderer>().enabled = false;
        yield return trail1.emitting = false;
        yield return trail2.emitting = false;
        hatAudio.Stop();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().Jump();
        }
    }
}
