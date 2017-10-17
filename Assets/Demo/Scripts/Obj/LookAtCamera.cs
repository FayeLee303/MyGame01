using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;

    private Quaternion _targetRotation;
    private Vector3 _rotation;
    private int _index;

    [Tooltip("镜头每次旋转的角度值，可修改数组长度和值")]
    public float[] _roteArray = new float[4] { 0, 90, 180, 270 };

    // Use this for initialization
    void Start ()
	{
	    cam = GameObject.Find("Cam").transform;
	    this.transform.rotation = cam.transform.rotation;

	}
	
	// Update is called once per frame
	void Update ()
	{
	    //_targetRotation = gameObject.transform.rotation;
	    //_rotation = _targetRotation.eulerAngles;
	    //if (_rotation != new Vector3(_rotation.x, _roteArray[_index], _rotation.z))
	    //{
	    //    return;
	    //}

        if (Input.GetKeyDown(KeyCode.Q))
	    {
	        //执行镜头旋转
	        if (_index == _roteArray.Length - 1)
	            _index = 0;
	        else
	            _index++;
	        _targetRotation = gameObject.transform.rotation;
	        _rotation = _targetRotation.eulerAngles;
	        _rotation = new Vector3(_rotation.x, _roteArray[_index], _rotation.z);
	        transform.DORotate(_rotation, 0.5f);

	    }

	    if (Input.GetKeyDown(KeyCode.E))
	    {
	        //执行镜头旋转   
	        if (_index == 0)
	            _index = _roteArray.Length - 1;
	        else
	            _index--;
	        _targetRotation = gameObject.transform.rotation;
	        _rotation = _targetRotation.eulerAngles;
	        _rotation = new Vector3(_rotation.x, _roteArray[_index], _rotation.z);
	        transform.DORotate(_rotation, 0.5f);
	    }

        //ObjEulerAnglesY = this.transform.rotation.eulerAngles.y;
        //CamEulerAnglesY = cam.transform.rotation.eulerAngles.y;
        //ObjEulerAnglesY = Mathf.Lerp(ObjEulerAnglesY, CamEulerAnglesY, 5f * Time.time);
        //if (Mathf.Abs(ObjEulerAnglesY - CamEulerAnglesY) < 0.02f)
        //{
        //    ObjEulerAnglesY = CamEulerAnglesY;
        //}
        //this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, ObjEulerAnglesY, this.transform.rotation.eulerAngles.z));
        //// this.transform.rotation = Quaternion.Slerp(this.transform.rotation,cam.transform.rotation,0.1f*Time.time);

    }
}
