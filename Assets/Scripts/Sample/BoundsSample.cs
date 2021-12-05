using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;
using Mathematica;

public class BoundsSample : MonoBehaviour
{
    GameObject AABB1;
    GameObject AABB2;

    GameObject OBB1;
    GameObject OBB2;

    GameObject Sphere1;
    GameObject Sphere2;

    GameObject Capsule1;
    GameObject Capsule2;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
        AABB1 = GameObject.Find("AABB1");
        AABB2 = GameObject.Find("AABB2");
        AABB1.GetComponent<MeshRenderer>().material.color = Color.green;
        AABB2.GetComponent<MeshRenderer>().material.color = Color.green;
        AABB aabb1 = new AABB(math.fix3(AABB1.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB1.GetComponent<BoxCollider>().bounds.max));
        AABB aabb2 = new AABB(math.fix3(AABB2.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB2.GetComponent<BoxCollider>().bounds.max));
        gos.Add(AABB1, aabb1);
        gos.Add(AABB2, aabb2);

        OBB1 = GameObject.Find("OBB1");
        OBB2 = GameObject.Find("OBB2");
        OBB1.GetComponent<MeshRenderer>().material.color = Color.blue;
        OBB2.GetComponent<MeshRenderer>().material.color = Color.blue;
        OBB obb1 = new OBB(math.fix3(OBB1.GetComponent<BoxCollider>().bounds.min), math.fix3(OBB1.GetComponent<BoxCollider>().bounds.max));
        OBB obb2 = new OBB(math.fix3(OBB2.GetComponent<BoxCollider>().bounds.min), math.fix3(OBB2.GetComponent<BoxCollider>().bounds.max));
        gos.Add(OBB1, obb1);
        gos.Add(OBB2, obb2);


        Sphere1 = GameObject.Find("Sphere1");
        Sphere2 = GameObject.Find("Sphere2");
        Sphere1.GetComponent<MeshRenderer>().material.color = Color.yellow;
        Sphere2.GetComponent<MeshRenderer>().material.color = Color.yellow;
        Sphere sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        Sphere sphere2 = new Sphere(math.fix3(Sphere2.transform.position), Sphere2.GetComponent<SphereCollider>().radius * 2);
        gos.Add(Sphere1, sphere1);
        gos.Add(Sphere2, sphere2);

        Capsule1 = GameObject.Find("Capsule1");
        Capsule2 = GameObject.Find("Capsule2");
        Capsule1.GetComponent<MeshRenderer>().material.color = Color.green;
        Capsule2.GetComponent<MeshRenderer>().material.color = Color.yellow;

        Capsule capsule1 = new Capsule(math.fix3(Capsule1.GetComponent<CapsuleCollider>().center), Capsule1.GetComponent<CapsuleCollider>().radius, Capsule1.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);
        Capsule capsule2 = new Capsule(math.fix3(Capsule2.GetComponent<CapsuleCollider>().center), Capsule2.GetComponent<CapsuleCollider>().radius, Capsule2.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);

        gos.Add(Capsule1, capsule1);
        gos.Add(Capsule2, capsule2);
    }
    void Update()
    {
        AABB aabb1 = new AABB(math.fix3(AABB1.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB1.GetComponent<BoxCollider>().bounds.max));
        AABB aabb2 = new AABB(math.fix3(AABB2.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB2.GetComponent<BoxCollider>().bounds.max));
        gos[AABB1] = aabb1;
        gos[AABB2] = aabb2;

        OBB obb1 = new OBB(math.fix3(OBB1.GetComponent<BoxCollider>().bounds.min), math.fix3(OBB1.GetComponent<BoxCollider>().bounds.max));
        OBB obb2 = new OBB(math.fix3(OBB2.GetComponent<BoxCollider>().bounds.min), math.fix3(OBB2.GetComponent<BoxCollider>().bounds.max));
        gos[OBB1] = obb1;
        gos[OBB2] = obb2;

        Sphere sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        Sphere sphere2 = new Sphere(math.fix3(Sphere2.transform.position), Sphere2.GetComponent<SphereCollider>().radius * 2);
        gos[Sphere1] = sphere1;
        gos[Sphere2] = sphere2;

        Capsule capsule1 = new Capsule(math.fix3(Capsule1.GetComponent<CapsuleCollider>().center), Capsule1.GetComponent<CapsuleCollider>().radius, Capsule1.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);
        Capsule capsule2 = new Capsule(math.fix3(Capsule2.GetComponent<CapsuleCollider>().center), Capsule2.GetComponent<CapsuleCollider>().radius, Capsule2.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);
        gos[Capsule1] = capsule1;
        gos[Capsule2] = capsule2;

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
