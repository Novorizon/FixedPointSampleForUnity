using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;
using Mathematica;

public class SphereOBBSample : MonoBehaviour
{
    GameObject OBB1;

    GameObject Sphere1;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
        OBB1 = GameObject.Find("OBB1");
        OBB obb1 = new OBB(math.fix3(OBB1.transform.position), fix3.one);
        gos.Add(OBB1, obb1);


        Sphere1 = GameObject.Find("Sphere1");
        Sphere sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        gos.Add(Sphere1, sphere1);
    }
    void Update()
    {
        OBB obb1 = new OBB(math.fix3(OBB1.transform.position), fix3.one);
        //obb1.Update(math.fix3(OBB1.transform.position), math.fix3(OBB1.transform.forward));
        obb1.Update(math.fix3(OBB1.transform.position), math.quaternion(OBB1.transform.rotation));
        gos[OBB1] = obb1;

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
