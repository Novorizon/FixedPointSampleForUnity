using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Physics = Mathematica.Physics.Physics;

public class SphereSample : MonoBehaviour
{
    Dictionary<GameObject, Sphere> aabbs = new Dictionary<GameObject, Sphere>();
    void Start()
    {
    }
    static float speed = 10;
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject go1 = GameObject.Find("Sphere1");
        GameObject go2 = GameObject.Find("Sphere2");
        go1.GetComponent<MeshRenderer>().material.color = Color.green;
        go2.GetComponent<MeshRenderer>().material.color = Color.yellow;

        Sphere b1 = new Sphere(math.fix3(go1.transform.position), go1.GetComponent<SphereCollider>().radius);
        Sphere b2 = new Sphere(math.fix3(go2.transform.position), go2.GetComponent<SphereCollider>().radius * 2);

        aabbs[go1] = b1;
        aabbs[go2] = b2;
        foreach (var item in aabbs)
        {
            foreach (var item1 in aabbs)
            {
                if (item.Key == item1.Key)
                    continue;
                if (Physics.IsOverlap(item.Value, item1.Value))
                {
                    item.Key.GetComponent<MeshRenderer>().material.color = Color.red;
                    item1.Key.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    item.Key.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    item1.Key.GetComponent<MeshRenderer>().material.color = Color.yellow;
                }

            }
        }

    }
}
