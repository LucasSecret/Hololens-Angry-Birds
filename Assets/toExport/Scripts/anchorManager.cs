using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class anchorManager : MonoBehaviour
{
    public GameObject _anchor;
    private Transform _anchorTransform;
    private GameObject cube;
    private TapToPlace tapToPlace;
    private bool _stateAnchor;

    // Start is called before the first frame update
    void Start()
    {
        //_anchorTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlaceAnchor()
    {
        _anchorTransform = _anchor.transform;
        _anchor.transform.gameObject.SetActive(false);
        Debug.Log(_anchorTransform.position);
        setStateAnchor(true);
    }

    public Transform getPositionAnchor()
    {
        return _anchorTransform;
    }

    public bool getStateAnchor()
    {
        return _stateAnchor;
    }

    public void setStateAnchor(bool state)
    {
        _stateAnchor = state;
    }
}
