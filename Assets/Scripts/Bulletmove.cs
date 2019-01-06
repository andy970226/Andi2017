using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletmove : MonoBehaviour {
    float angle;
    public Vector3 speed;
    public bool dead;
    // Use this for initialization
    void Start () {
        GameObject.Destroy(gameObject, 16.0f);
        dead = false;
      
    }
    void Awake() {
        angle = GameObject.Find("CononBase").transform.rotation.z;
        angle = angle * 115.718f;
        angle = angle + 147;
        speed=new Vector3(Mathf.Cos(angle * 3.1416f / 180) * 11, Mathf.Sin(angle * 3.1416f / 180) * 11, 0);
    }
	// Update is called once per frame
	void Update () {
        //GameObject.Find("Turkey1").GetComponent<Turkey>().ballpos.Add



        float windoffset=GameObject.Find("cloud").GetComponent<Wind>().windspeed;      
        if(transform.position.x>-4&& transform.position.x < 4)
        speed.x += windoffset*Time.deltaTime/3;

        if (!dead) speed.y -= 5 * Time.deltaTime;
        transform.Translate(speed * Time.deltaTime);
        //lastspeed = speed;

      
    }
    public void destroy() {
        GameObject.Destroy(gameObject, 0f);
    }
}
