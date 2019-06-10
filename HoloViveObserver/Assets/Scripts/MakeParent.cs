using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeParent : MonoBehaviour {

	public Transform ControllerLeft;
	public Transform ControllerRight;
	//public float mid = 0.5;
	Vector3 tempPos;
	Vector3 tempRot;
	Quaternion temp;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		tempPos = Vector3.Lerp (ControllerRight.transform.position, ControllerLeft.transform.position, 0.5f);
		temp = Quaternion.Lerp (ControllerRight.transform.rotation, ControllerLeft.transform.rotation, 0.5f);
		//Vector3 posY = (ControllerLeft.transform.position.y + ControllerRight.transform.position.y)/2;
		//Vector3 posZ = (ControllerLeft.transform.position.z + ControllerRight.transform.position.z)/2;
		//Vector3 rotX = (ControllerLeft.transform.rotation.x + ControllerRight.transform.rotation.x)/2;
		//Vector3 rotY = (ControllerLeft.transform.rotation.y + ControllerRight.transform.rotation.y)/2;
		//Vector3 rotZ = (ControllerLeft.transform.rotation.z + ControllerRight.transform.rotation.z)/2;
		//transform.position.x = ControllerLeft.transform.position.x + (ControllerRight.transform.position.x - ControllerLeft.transform.position.x) / 2;
		this.transform.position = tempPos;
		this.transform.rotation = temp;
		//this.transform.position.y = posY;
		//this.transform.position.z = posZ;
		//this.transform.rotation.x = rotX;
		//this.transform.rotation.y = rotY;
		//this.transform.rotation.z = rotZ;

		//this.transform.position = ControllerLeft.transform.position;
		//this.transform.rotation = ControllerLeft.transform.rotation;
	}

	public void linkObjects () {
		//this.transform.parent = Controller.transform;

	}
}
