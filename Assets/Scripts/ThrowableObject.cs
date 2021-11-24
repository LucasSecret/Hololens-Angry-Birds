using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
public class ThrowableObject : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool isResettingPosition;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        isResettingPosition = false;
    }



    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 31 && !isResettingPosition)
            StartCoroutine(ResetPositionAfter(5));
    }

    private IEnumerator ResetPositionAfter(int seconds)
    {
        isResettingPosition = true;
        this.GetComponent<ObjectManipulator>().enabled = false;
        yield return new WaitForSeconds(seconds);
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<ObjectManipulator>().enabled = true;
        isResettingPosition = false;

    }
}
