using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey : MonoBehaviour
{
    public Material mat;
    // Use this for initialization
    public List<Vector3> turkey = new List<Vector3>();
    public List<Vector3> turkeymodel = new List<Vector3>();
    public List<Vector3> turkeylastpos1 = new List<Vector3>();
    public List<Vector3> turkeylastpos2 = new List<Vector3>();



    public List<Vector3> hilldetect = new List<Vector3>();
    private LineRenderer line;
    LineRenderer line2;
    GameObject line2obj;

    float rate = 3;
    float rate2 = 0.5f;
    float next = 0;
    float next1 = 0;

    void Awake()
    {
        turkey.Add(new Vector3(0, 0, 0));//zuotui
        turkey.Add(new Vector3(-1, 0, 0));
        turkey.Add(new Vector3(-3, 3, 0));
        turkey.Add(new Vector3(-3, 4, 0));//bozi
        turkey.Add(new Vector3(-4, 1.5f, 0));//wattle
        turkey.Add(new Vector3(-4, 4, 0));
        turkey.Add(new Vector3(-5, 4.5f, 0));//moth
        turkey.Add(new Vector3(-3.5f, 5.5f, 0));
        turkey.Add(new Vector3(-2.5f, 5.5f, 0));
        turkey.Add(new Vector3(-1.5f, 3, 0));
        turkey.Add(new Vector3(-0.5f, 5.5f, 0));
        turkey.Add(new Vector3(0.5f, 7, 0));
        turkey.Add(new Vector3(2, 8, 0));
        turkey.Add(new Vector3(4, 8, 0));
        turkey.Add(new Vector3(7, 6, 0));
        turkey.Add(new Vector3(7.5f, 3, 0));
        turkey.Add(new Vector3(6.5f, 1.5f, 0));
        turkey.Add(new Vector3(6, 1, 0));
        turkey.Add(new Vector3(3, 0, 0));
        turkey.Add(new Vector3(0, 0, 0));//19
        turkey.Add(new Vector3(0, -1, 0));
        turkey.Add(new Vector3(-1, -3, 0));//21
        turkey.Add(new Vector3(1, 0, 0));
        turkey.Add(new Vector3(1, -1, 0));
        turkey.Add(new Vector3(0, -3, 0));
        turkey.Add(new Vector3(-3.5f, 5, 0));//25
        turkey.Add(new Vector3(-3.4f, 5, 0));
        turkey.Add(new Vector3(-3.45f, 4.8f, 0));//27
        turkey.Add(new Vector3(3f, 3f, 0));//28
        model();




    }


    void Start()
    {



        line = this.gameObject.AddComponent<LineRenderer>();

        line.material = new Material(Shader.Find("Particles/Additive"));


        line.positionCount = 27;

        line.startColor = Color.yellow;
        line.endColor = Color.yellow;


        line.startWidth = 0.05f;
        line.endWidth = 0.05f;




        line2obj = new GameObject("line2obj");
        line2obj.transform.SetParent(transform);//将他作为当前物体的子物体

        line2 = line2obj.gameObject.AddComponent<LineRenderer>();

        line2.material = new Material(Shader.Find("Particles/Additive"));


        line2.positionCount = 3;

        line2.startColor = Color.red;
        line2.endColor = Color.red;


        line2.startWidth = 0.1f;
        line2.endWidth = 0.1f;

        //drawturkey();
        hilldetect = GameObject.Find("hill").GetComponent<mesh1>().hillseg;
        // speedy = 0;
    }

    bool leftorright;
    //float speed;
    //float speedy;
    bool turn = true;
    Vector3 acc;
    void Update()
    {


        if (turn)
        {
            turkeylastpos1.Clear();
            foreach (Vector3 x in turkey)
                turkeylastpos1.Add(x);

            turn = !turn;
        }
        else
        {
            turkeylastpos2.Clear();
            foreach (Vector3 x in turkey)
                turkeylastpos2.Add(x);

            turn = !turn;

        }


        acc = new Vector3(0, 0, 0);




        if ((turkey[21] * 0.15f + transform.position).y > -5)
        {
            //speedy -= 2 * Time.deltaTime;
            acc.y = -15f;
        }
        if (Time.time > next1)
        {
            if (Random.Range(-2, 10) == 0 && (turkey[21] * 0.15f + transform.position).y < -5.1)
        {
        //speedy = 120 * Time.deltaTime;
        acc.y = 2000;
        }
            next1 = Time.time + rate2;
        }

        if (Time.time > next)
        {
            //speed = Time.deltaTime * 5;
            if (Random.Range(-5, 10) > 0)
                leftorright = true;
            else
                leftorright = false;
            next = Time.time + rate;

        }




        if (leftorright)
        {
            if ((turkey[14] * 0.15f + transform.position).x < 0)
                acc.x = 1;
            // turkey[28] = new Vector3(turkey[28].x + speed, turkey[28].y + speedy);
            else
            {
                
             //   acc.x = -20;
            }

        }
        if (!leftorright)
        {
            if ((turkey[6] * 0.15f + transform.position).x > -12)
                acc.x = -1;//left
            //   turkey[28] = new Vector3(turkey[28].x - speed, turkey[28].y + speedy);
            else
            {
              //  acc.x = 20;
               
            }
            }

        if ((turkey[28] * 0.15f + transform.position).y > 0.5)
        {
            //speedy -= 2 * Time.deltaTime;
            acc.x += GameObject.Find("cloud").GetComponent<Wind>().windspeed;
        }







        collidewithcanonball();

        collidewithhill();


      

        if ((turkey[14] * 0.15f + transform.position).x > 0)
        {
            turkey[28] = new Vector3(turkeylastpos1[28].x, turkey[28].y);
            turkeylastpos2[28] = new Vector3(turkeylastpos1[28].x, turkeylastpos2[28].y);
            acc.x = -200;
        }
        if ((turkey[6] * 0.15f + transform.position).x < -13)
        {
            turkey[28] = new Vector3(turkeylastpos1[28].x, turkey[28].y);
            turkeylastpos2[28] = new Vector3(turkeylastpos1[28].x, turkeylastpos2[28].y);
            acc.x = 200;
        }




        verlet(28, acc);
        if ((turkey[28] * 0.15f + transform.position).y < -5.1)
        {
            turkey[28] = new Vector3(turkey[28].x, turkeylastpos1[28].y);
            turkeylastpos2[28] = new Vector3(turkeylastpos2[28].x, turkeylastpos1[28].y);


        }

 

        check_constraint(28);
        drawturkey();


    }
    void drawturkey()
    {



        for (int i = 0; i < 20; i++)
            line.SetPosition(i, turkey[i] * 0.15f + transform.position);
        line.SetPosition(20, turkey[20] * 0.15f + transform.position);
        line.SetPosition(21, turkey[21] * 0.15f + transform.position);
        line.SetPosition(22, turkey[19] * 0.15f + transform.position);

        line.SetPosition(23, turkey[22] * 0.15f + transform.position);
        line.SetPosition(24, turkey[23] * 0.15f + transform.position);
        line.SetPosition(25, turkey[24] * 0.15f + transform.position);
        line.SetPosition(26, turkey[22] * 0.15f + transform.position);


        line2.SetPosition(0, turkey[25] * 0.15f + transform.position);
        line2.SetPosition(1, turkey[26] * 0.15f + transform.position);
        line2.SetPosition(2, turkey[27] * 0.15f + transform.position);
    }

    void verlet(int point, Vector3 acc1)
    {
        if (turn)

            turkey[point] = 2 * turkeylastpos2[point] - turkeylastpos1[point] + acc1 * Time.deltaTime * Time.deltaTime;// * Time.deltaTime
                                                                                                                       //   turkey[point] += acc * Time.deltaTime;
        else
            turkey[point] = 2 * turkeylastpos1[point] - turkeylastpos2[point] + acc1 * Time.deltaTime * Time.deltaTime; //+ acc * Time.deltaTime;  * Time.deltaTime
                                                                                                                        //   turkey[point] += acc * Time.deltaTime;
       // Debug.Log(acc1);
    }





    public void check_constraint(int vertex)
    {


        for (int i = 0; i < 29; i++)
        {
            Vector3 edge1 = turkey[i] - turkey[vertex];
            Vector3 edge1m = turkeymodel[i] - turkeymodel[vertex];
            //if (Mathf.Abs(Vector3.Angle(edge1, edge1m)) > 20)
            { turkey[i] += 0.02f * Time.deltaTime * (edge1m - edge1) * Mathf.Abs(Vector3.Angle(edge1, edge1m)); }
            // if ( Mathf.Abs(edge1.magnitude-edge1m.magnitude)>1)
            {

                turkey[i] += 0.3f * Time.deltaTime * (edge1m - edge1) * Mathf.Abs(edge1.magnitude - edge1m.magnitude);
            }
        }




    }
    void model()
    {

        turkeymodel.Add(new Vector3(0, 0, 0));//zuotui
        turkeymodel.Add(new Vector3(-1, 0, 0));
        turkeymodel.Add(new Vector3(-3, 3, 0));
        turkeymodel.Add(new Vector3(-3, 4, 0));//bozi
        turkeymodel.Add(new Vector3(-4, 1.5f, 0));//wattle
        turkeymodel.Add(new Vector3(-4, 4, 0));
        turkeymodel.Add(new Vector3(-5, 4.5f, 0));//moth
        turkeymodel.Add(new Vector3(-3.5f, 5.5f, 0));
        turkeymodel.Add(new Vector3(-2.5f, 5.5f, 0));
        turkeymodel.Add(new Vector3(-1.5f, 3, 0));
        turkeymodel.Add(new Vector3(-0.5f, 5.5f, 0));
        turkeymodel.Add(new Vector3(0.5f, 7, 0));
        turkeymodel.Add(new Vector3(2, 8, 0));
        turkeymodel.Add(new Vector3(4, 8, 0));
        turkeymodel.Add(new Vector3(7, 6, 0));
        turkeymodel.Add(new Vector3(7.5f, 3, 0));
        turkeymodel.Add(new Vector3(6.5f, 1.5f, 0));
        turkeymodel.Add(new Vector3(6, 1, 0));
        turkeymodel.Add(new Vector3(3, 0, 0));
        turkeymodel.Add(new Vector3(0, 0, 0));//19
        turkeymodel.Add(new Vector3(0, -1, 0));
        turkeymodel.Add(new Vector3(-1, -3, 0));//21
        turkeymodel.Add(new Vector3(1, 0, 0));
        turkeymodel.Add(new Vector3(1, -1, 0));
        turkeymodel.Add(new Vector3(0, -3, 0));
        turkeymodel.Add(new Vector3(-3.5f, 5, 0));//25
        turkeymodel.Add(new Vector3(-3.4f, 5, 0));
        turkeymodel.Add(new Vector3(-3.45f, 4.8f, 0));
        turkeymodel.Add(new Vector3(3f, 3f, 0));//28

        foreach (Vector3 x in turkey)
        {
            turkeylastpos1.Add(x);
            turkeylastpos2.Add(x);
        }
    }

    void collidewithhill()
    {

        for (int i = 0; i < 29; i++)
        {
            Vector3 point = transform.position + turkey[i] * 0.15f;
            for (int j = 0; j < hilldetect.Count; j++)
                if ((point - hilldetect[j]).magnitude < 0.4)
                {
                    //Debug.Log("touch");
                    //   turkey[28] = new Vector3(turkey[28].x - 3, turkey[28].y + 3);
                    turkey[28] = turkeylastpos1[28];
                    turkeylastpos2[28] = turkeylastpos1[28];
                    acc .Set(acc.x - 200, acc.y + 200,0);
                    //check_constraint(28);
                }

        }


    }


    void collidewithcanonball()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("ball");
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].GetComponent<Bulletmove>().dead == false)
            {
                for (int z = 0; z < 29; z++)
                {
                    Vector3 point = transform.position + turkey[z] * 0.15f;

                    if ((point - array[i].transform.position).magnitude < 0.3)
                    {
                        Debug.Log("boom");
                        acc.Set(acc.x+ array[i].GetComponent<Bulletmove>().speed.x*100, acc.y + array[i].GetComponent<Bulletmove>().speed.y*100,0);
                        //turkey[z] = new Vector3(turkey[z].x - 5, turkey[z].y);
                        verlet(z, acc);
                        // turkey[28] = new Vector3(turkey[28].x - 5, turkey[28].y);
                           check_constraint(z);
                        array[i].GetComponent<Bulletmove>().destroy();
                    }

                }



            }




        }

    }




}









