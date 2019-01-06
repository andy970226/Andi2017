using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.DownArrow) && transform.rotation.z < 0.27)
        {
            transform.Rotate(new Vector3(0, 0, 20) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) && transform.rotation.z >-0.5)
            transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
    }
}
