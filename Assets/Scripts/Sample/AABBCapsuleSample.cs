using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;
using Mathematica;

public class AABBCapsuleSample : MonoBehaviour
{
    GameObject AABB1;
    GameObject Capsule1;
    AABB aabb1;
    Capsule capsule1;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
        AABB1 = GameObject.Find("AABB1");
        aabb1 = new AABB(math.fix3(AABB1.transform.position), fix3.one * new fix3(1, 1, 100));
        gos.Add(AABB1, aabb1);
        Capsule1 = GameObject.Find("Capsule1");
        capsule1 = new Capsule(math.fix3(Capsule1.GetComponent<CapsuleCollider>().center), Capsule1.GetComponent<CapsuleCollider>().radius * 1, Capsule1.GetComponent<CapsuleCollider>().height * 5, quaternion.identity, fix3.up);

        gos.Add(Capsule1, capsule1);
    }
    void Update()
    {
        aabb1.Update(math.fix3(AABB1.transform.position));
        gos[AABB1] = aabb1;
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
