using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using quaternion = Mathematica.quaternion;
using Physics = Mathematica.Physics.Physics;

public class CapsuleSample : MonoBehaviour
{
    Dictionary<GameObject, Capsule> aabbs = new Dictionary<GameObject, Capsule>();
    void Start()
    {
        GameObject go1 = GameObject.Find("Capsule1");
        GameObject go2 = GameObject.Find("Capsule2");
        go1.GetComponent<MeshRenderer>().material.color = Color.green;
        go2.GetComponent<MeshRenderer>().material.color = Color.yellow;

        Capsule b1 = new Capsule(math.fix3(go1.GetComponent<CapsuleCollider>().center), go1.GetComponent<CapsuleCollider>().radius, go1.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);
        Capsule b2 = new Capsule(math.fix3(go2.GetComponent<CapsuleCollider>().center), go2.GetComponent<CapsuleCollider>().radius, go2.GetComponent<CapsuleCollider>().height, quaternion.identity, fix3.up);

        aabbs.Add(go1, b1);
        aabbs.Add(go2, b2);
    }
    void Update()
    {
        GameObject go1 = GameObject.Find("Capsule1");
        GameObject go2 = GameObject.Find("Capsule2");
        Capsule b1 = new Capsule(math.fix3(go1.transform.position), go1.GetComponent<CapsuleCollider>().radius, go1.GetComponent<CapsuleCollider>().height, math.quaternion(go1.transform.rotation), fix3.up);
        Capsule b2 = new Capsule(math.fix3(go2.transform.position), go2.GetComponent<CapsuleCollider>().radius*2, go2.GetComponent<CapsuleCollider>().height*2, math.quaternion(go2.transform.rotation), fix3.up);

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
