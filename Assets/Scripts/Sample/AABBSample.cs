using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Physics = Mathematica.Physics.Physics;

public class AABBSample : MonoBehaviour
{
    Dictionary<GameObject, AABB> aabbs = new Dictionary<GameObject, AABB>();
    void Start()
    {
        GameObject Cube1 = GameObject.Find("AABB1");
        GameObject Cube2 = GameObject.Find("AABB2");
        Cube1.GetComponent<MeshRenderer>().material.color = Color.green;

        Cube2.GetComponent<MeshRenderer>().material.color = Color.yellow;

        AABB aabb1 = new AABB(math.fix3(Cube1.transform.position), fix3.one);
        AABB aabb2 = new AABB(math.fix3(Cube2.transform.position), fix3.one);
        aabbs.Add(Cube1, aabb1);
        aabbs.Add(Cube2, aabb2);
    }
    void Update()
    {
        GameObject Cube1 = GameObject.Find("AABB1");
        GameObject Cube2 = GameObject.Find("AABB2");
        Cube1.GetComponent<MeshRenderer>().material.color = Color.green;

        Cube2.GetComponent<MeshRenderer>().material.color = Color.yellow;

        AABB aabb1 = new AABB(math.fix3(Cube1.transform.position), fix3.one);
        AABB aabb2 = new AABB(math.fix3(Cube2.transform.position), fix3.one);
        aabbs[Cube1] = aabb1;
        aabbs[Cube2] = aabb2;
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
