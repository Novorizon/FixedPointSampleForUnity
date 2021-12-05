using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;
using Mathematica;

public class SphereAABBSample : MonoBehaviour
{
    GameObject AABB1;
    GameObject AABB2;

    GameObject Sphere1;
    GameObject Sphere2;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
        AABB1 = GameObject.Find("AABB1");
        AABB1.GetComponent<MeshRenderer>().material.color = Color.green;
        AABB aabb1 = new AABB(math.fix3(AABB1.transform.position), fix3.one);
        gos.Add(AABB1, aabb1);


        Sphere1 = GameObject.Find("Sphere1");
        Sphere1.GetComponent<MeshRenderer>().material.color = Color.yellow;
        Sphere sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        gos.Add(Sphere1, sphere1);
    }
    void Update()
    {
        AABB aabb1 = new AABB(math.fix3(AABB1.transform.position), fix3.one);
        gos[AABB1] = aabb1;

        Sphere sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        gos[Sphere1] = sphere1;


        foreach (var item in gos)
        {
            foreach (var item1 in gos)
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
