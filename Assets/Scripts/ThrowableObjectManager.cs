using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ThrowableObjectManager : MonoBehaviour
{
    public GameObject _throwableObject;

    private Rigidbody _objectRigidbody;

    private Vector3[] _linearVelocityFrames;
    private Vector3[] _angularVelocityFrames;

    private int _currentVelocityFrameStep;

    private int _historyFramesNumber = 10;

    private Vector3 _initialPosition;

    private float _velocityFactor;

    // Start is called before the first frame update
    void Start()
    {
        _objectRigidbody = _throwableObject.transform.GetComponent<Rigidbody>();
        _linearVelocityFrames = new Vector3[_historyFramesNumber];
        _angularVelocityFrames = new Vector3[_historyFramesNumber];
        _currentVelocityFrameStep = 0;

        _objectRigidbody.isKinematic = true;

        _initialPosition = _throwableObject.transform.position;
    }


    public void ObjectGrabbed(ManipulationEventData eventData)
    {
        _objectRigidbody.isKinematic = true;

        _velocityFactor = 1 + Vector3.Distance(eventData.Pointer.Position, _throwableObject.transform.position);
    }


    public void ObjectReleased()
    {
        _objectRigidbody.isKinematic = false;
        _objectRigidbody.useGravity = true;

        StartCoroutine(WaitForKinematic());
    }

    // Update is called once per frame
    void Update()
    {
        VelocityUpdate();
    }


    private void VelocityUpdate()
    {
        if (_linearVelocityFrames == null || _angularVelocityFrames == null)
            return;

        _currentVelocityFrameStep++;

        if (_currentVelocityFrameStep >= _linearVelocityFrames.Length)
            _currentVelocityFrameStep = 0;

        _linearVelocityFrames[_currentVelocityFrameStep] = _objectRigidbody.velocity;
        _angularVelocityFrames[_currentVelocityFrameStep] = _objectRigidbody.angularVelocity;
    }


    private Vector3 GetVectorAverage(Vector3[] values)
    {
        Vector3 average = Vector3.zero;
        foreach (Vector3 value in values)
            average += value;

        return average / values.Length;
    }


    private void AddVelocityHistory()
    {
        if (_linearVelocityFrames == null || _angularVelocityFrames == null)
            return;

        _objectRigidbody.velocity = GetVectorAverage(_linearVelocityFrames);
        _objectRigidbody.angularVelocity = GetVectorAverage(_angularVelocityFrames);

    }

    private void ResetVelocityHistory()
    {
        _currentVelocityFrameStep = 0;

        _linearVelocityFrames = new Vector3[_historyFramesNumber];
        _angularVelocityFrames = new Vector3[_historyFramesNumber];
    }


    private IEnumerator WaitForKinematic()
    {
        while(!_objectRigidbody.isKinematic)
        {
            yield return null;
        }

        ThrowObject();

    }


    private void ThrowObject()
    {
        _objectRigidbody.isKinematic = false;
        _objectRigidbody.useGravity = true;

        _objectRigidbody.velocity = _objectRigidbody.velocity + Vector3.Cross(_objectRigidbody.angularVelocity,
                                                                              _throwableObject.transform.position - _objectRigidbody.centerOfMass);

        _objectRigidbody.velocity *= 1/_velocityFactor;
        AddVelocityHistory();
        ResetVelocityHistory();
    }


    private IEnumerator ResetPositionAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _throwableObject.transform.position = _initialPosition;
    }


    public void ResetCubePosition()
    {
        GameObject gO = Instantiate(_throwableObject);
        Destroy(_throwableObject);
        _throwableObject = gO;
        _throwableObject.transform.position = _initialPosition;
    }
    
}
