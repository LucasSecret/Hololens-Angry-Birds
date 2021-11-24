using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handMenuManager : MonoBehaviour
{
    public GameObject _prefabLvlOne;
    public GameObject _prefabLvlTwo;
    public GameObject _prefabLvlThree;
    public List<GameObject> _lstAvatar;
    public anchorManager anchorManager;

    private int _level = -1; //Do : +1 at each instantiation    
    private int _avatar = -1; //Do : +1 at each instantiation    
    private GameObject _actualGO;

    public GameObject _throwableObject;

    // Start is called before the first frame update
    void Start()
    {
        _throwableObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(anchorManager.getStateAnchor())
        {
            onClickLoadLvl();
            Debug.Log("Update");
            anchorManager.setStateAnchor(false);
            _throwableObject.SetActive(true);

        }
    }

    public void onClickLoadLvl()
    {
        GameObject _instantiatedGO = new GameObject();
        _level += 1;

        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        switch (_level % 3) 
        {
            default:
                break;
            case 0 :
                _instantiatedGO = Instantiate(_prefabLvlOne);
                _instantiatedGO.name = "prefabOne";
                break;
            case 1:
                _instantiatedGO = Instantiate(_prefabLvlTwo);
                _instantiatedGO.name = "prefabTwo";
                break;
            case 2:
                _instantiatedGO = Instantiate(_prefabLvlThree);
                _instantiatedGO.name = "prefabThree";
                break;
        }
        _instantiatedGO.transform.position = anchorManager.getPositionAnchor().position;
        _instantiatedGO.transform.SetParent(this.transform);
        Debug.Log(_instantiatedGO.transform.position);
        Debug.Log("LoadLvl : " + _level % 3);
    }

    public void onClickLoadAvatar()
    {
        _avatar += 1;
        switch (_level % 3)
        {
            default:
                break;
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
        Debug.Log("Avatar : " + _avatar % 3);
    }
}
