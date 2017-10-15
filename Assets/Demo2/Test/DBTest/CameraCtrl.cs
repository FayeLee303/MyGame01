using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    public Transform _camera;
    public float rotateAngle;
    

	// Use this for initialization
	void Start () {
       //_camera = GetComponet<Transform>();
        rotateAngle = 90f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CamRotate(string Dir) {

    }
}
