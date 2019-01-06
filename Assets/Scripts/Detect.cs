using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour {

    List<List<Vector3>> line = new List<List<Vector3>>();
    List<List<Vector3>> turkeyline = new List<List<Vector3>>();
    float radius = 0.17f;
    // Use this for initialization


        List<Vector3> list1 = new List<Vector3>();
        List<Vector3> list2 = new List<Vector3>();

    void Start() {
        
        for (int i=0;i< GameObject.Find("hill").GetComponent<mesh1>().hillseg.Count-1;i++)
        list1.Add( GameObject.Find("hill").GetComponent<mesh1>().hillseg[i]);

        list1.Add(new Vector3(-13, -6f));
        line.Add(list1);
        for (int i = 1; i < GameObject.Find("hill").GetComponent<mesh1>().hillseg.Count; i++)
            list2.Add(GameObject.Find("hill").GetComponent<mesh1>().hillseg[i]);

        list2.Add(new Vector3(-13, 20f));
        line.Add(list2);
        detect = false;
        nondetectedperiod = false;
    }
    bool detect;//if in a collide position
    bool nondetectedperiod;// if in a collide period
    // Update is called once per frame
    void Update() {
        if (transform.position.y<=-5.5)
        {
            GetComponent<Bulletmove>().speed = new Vector3(0,0);
            GetComponent<Bulletmove>().dead = true;
            
        }
        detect = false;
        
        
            for (int i = 0; i < list1.Count; i++)
            {
                Vector3 a = line[0][i];
                Vector3 b = line[1][i];
                Vector3 c = new Vector3(transform.position.x, transform.position.y, 0);

                Vector3 ab = b - a;
                Vector3 ac = c - a;
                Vector3 bc = c - b;
                float f = Vector3.Dot(ab, ac);
                float d = Vector3.Dot(ab, ab);
                float f1 = f / d;

                Vector3 D = a + f1 * ab;   // c在ab线段上的投影点


                if (f >= 0 && f <= d)
                if (Vector3.Distance(c, D) < radius)
                {
                    detect = true;
                    if (!nondetectedperiod)
                    {

                        float theta = Mathf.Atan2(ab.y, ab.x) * 2 - Mathf.Atan2(GetComponent<Bulletmove>().speed.y, GetComponent<Bulletmove>().speed.x);
                        float x = GetComponent<Bulletmove>().speed.magnitude * Mathf.Cos(theta);
                        float y = GetComponent<Bulletmove>().speed.magnitude * Mathf.Sin(theta);
                        this.GetComponent<Bulletmove>().speed.x = x*0.95f ;
                        this.GetComponent<Bulletmove>().speed.y = y * 0.95f;
                        Debug.Log("edge");
                        nondetectedperiod = true;
                    }
                }



            else if (f < 0)
                {
                    if (Vector3.Distance(c, a) < 1.5f*radius)
                    {
                    detect = true;
                    if (!nondetectedperiod)
                        {
                            float theta = Mathf.Atan2(ac.y, ac.x) * 2 - Mathf.Atan2(GetComponent<Bulletmove>().speed.y, GetComponent<Bulletmove>().speed.x);
                            float x = GetComponent<Bulletmove>().speed.magnitude * Mathf.Cos(theta);
                            float y = GetComponent<Bulletmove>().speed.magnitude * Mathf.Sin(theta);
                            this.GetComponent<Bulletmove>().speed.x = x * 0.95f;
                            this.GetComponent<Bulletmove>().speed.y = y * 0.95f;
                            //GameObject.Destroy(gameObject, 0);
                            Debug.Log("corner2");
                            nondetectedperiod = true;
                        
                        }


                    }


                }


                else if (f > d)
                {
                    if (Vector3.Distance(c, b) < 1.5f * radius)
                    {
                    detect = true;
                    if (!nondetectedperiod)
                        {
                            //GameObject.Destroy(gameObject, 0);
                            float theta = Mathf.Atan2(bc.y, bc.x) * 2 - Mathf.Atan2(GetComponent<Bulletmove>().speed.y, GetComponent<Bulletmove>().speed.x);
                            float x = GetComponent<Bulletmove>().speed.magnitude * Mathf.Cos(theta);
                            float y = GetComponent<Bulletmove>().speed.magnitude * Mathf.Sin(theta);
                            this.GetComponent<Bulletmove>().speed.x = x * 0.95f;
                            this.GetComponent<Bulletmove>().speed.y = y * 0.95f;
                            Debug.Log("corner1");
                            nondetectedperiod = true;
                        }
                    }

                }



                





            }
        if (!detect)
            nondetectedperiod = false;
        
    }

}
