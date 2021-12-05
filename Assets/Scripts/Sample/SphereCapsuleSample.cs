using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;
using Mathematica;

public class SphereCapsuleSample : MonoBehaviour
{
    GameObject Sphere1;
    GameObject Capsule1;
    Sphere sphere1;
    Capsule capsule1;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
        Sphere1 = GameObject.Find("Sphere1");
         sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        gos.Add(Sphere1, sphere1);
        Capsule1 = GameObject.Find("Capsule1");
        capsule1 = new Capsule(math.fix3(Capsule1.GetComponent<CapsuleCollider>().center), Capsule1.GetComponent<CapsuleCollider>().radius, Capsule1.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);

        gos.Add(Capsule1, capsule1);
    }
    void Update()
    {
        sphere1.Update(math.fix3(Sphere1.transform.position));
        gos[Sphere1] = sphere1;
        capsule1.Update(math.fix3(Capsule1.transform.position), math.quaternion(Capsule1.transform.rotation));
        gos[Capsule1] = capsule1;

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
