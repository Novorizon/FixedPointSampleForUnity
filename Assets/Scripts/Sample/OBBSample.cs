using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;

public class OBBSample : MonoBehaviour
{
    GameObject OBB1;
    GameObject OBB2;
    OBB obb1;
    OBB obb2;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
        OBB1 = GameObject.Find("OBB1");
        OBB2 = GameObject.Find("OBB2");

        obb1 = new OBB(math.fix3(OBB1.transform.position), fix3.one);
        obb2 = new OBB(math.fix3(OBB2.transform.position), fix3.one);
        gos.Add(OBB1, obb1);
        gos.Add(OBB2, obb2);
    }
    
    void Update()
    {
        //obb1.Update(math.fix3(OBB1.transform.position), math.fix3(OBB1.transform.forward), math.fix3(OBB1.transform.up));
        //obb2.Update(math.fix3(OBB2.transform.position), math.fix3(OBB2.transform.forward), math.fix3(OBB2.transform.up));
        obb1.Update(math.fix3(OBB1.transform.position), math.quaternion(OBB1.transform.rotation));
        obb2.Update(math.fix3(OBB2.transform.position), math.quaternion(OBB2.transform.rotation));

        gos[OBB1] = obb1;
        gos[OBB2] = obb2;
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
