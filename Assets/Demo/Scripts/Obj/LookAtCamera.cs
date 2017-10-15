using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;
    private float ObjEulerAnglesY;
    private float CamEulerAnglesY;

	// Use this for initialization
	void Start ()
	{
	    cam = GameObject.Find("Cam").transform;
	    this.transform.rotation = cam.transform.rotation;

	}
	
	// Update is called once per frame
	void Update ()
	{
	    ObjEulerAnglesY = this.transform.rotation.eulerAngles.y;
	    CamEulerAnglesY = cam.transform.rotation.eulerAngles.y;
	    ObjEulerAnglesY = Mathf.Lerp(ObjEulerAnglesY, CamEulerAnglesY, 5f * Time.time);
	    if (Mathf.Abs(ObjEulerAnglesY - CamEulerAnglesY) < 0.02f)
	    {
	        ObjEulerAnglesY = CamEulerAnglesY;
	    }
	    this.transform.rotation = Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, ObjEulerAnglesY, this.transform.rotation.eulerAngles.z));
	    // this.transform.rotation = Quaternion.Slerp(this.transform.rotation,cam.transform.rotation,0.1f*Time.time);

	}
}
