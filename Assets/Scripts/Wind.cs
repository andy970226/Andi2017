using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
    public float windspeed;
	// Use this for initialization
	void Start () {
		
	}
    float rate = 0.5f;
    float next = 0;
	// Update is called once per frame
	void Update () {
        if (Time.time > next)
        {
            windspeed = Random() - 10;
            next = Time.time + rate;
        }

        transform.Translate(new Vector3(windspeed,0,0)*Time.deltaTime);
        if (transform.position.x >= 12)
            transform.Translate(new Vector3(-24,0,0));
        if (transform.position.x <= -12)
            transform.Translate(new Vector3(24, 0, 0));


    }



    int getRangeNum = 0;
    int rangeRadomNum = 0;

    int Random()
    {
        do
        {
            rangeRadomNum = UnityEngine.Random.Range(0, 20);
        }
        while (getRangeNum == rangeRadomNum);
        getRangeNum = rangeRadomNum;

            return getRangeNum;
    }

}

