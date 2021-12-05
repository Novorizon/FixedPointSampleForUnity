using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;

public class AABBOBBSample: MonoBehaviour
{
    GameObject OBB1;
    OBB obb1;
    GameObject AABB1;
    AABB aabb1;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    void Start()
    {
         AABB1 = GameObject.Find("AABB1");
         OBB1 = GameObject.Find("OBB1");

         aabb1 = new AABB(math.fix3(AABB1.transform.position), fix3.one);
         obb1 = new OBB(math.fix3(OBB1.transform.position), fix3.one);
        gos.Add(AABB1, aabb1);
        gos.Add(OBB1, obb1);
    }
    //private void OnDrawGizmos()
    //{
    //    if (obb1.Points != null)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[0]), math.Vector3(obb1.Points[1]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[1]), math.Vector3(obb1.Points[2]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[2]), math.Vector3(obb1.Points[3]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[3]), math.Vector3(obb1.Points[0]));

    //        Gizmos.DrawLine(math.Vector3(obb1.Points[4]), math.Vector3(obb1.Points[5]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[5]), math.Vector3(obb1.Points[6]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[6]), math.Vector3(obb1.Points[7]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Points[7]), math.Vector3(obb1.Points[4]));

    //        Gizmos.DrawLine(math.Vector3(obb1.Center), math.Vector3(obb1.Center + 10 * obb1.Normals[0]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Center), math.Vector3(obb1.Center + 10 * obb1.Normals[1]));
    //        Gizmos.DrawLine(math.Vector3(obb1.Center), math.Vector3(obb1.Center + 10 * obb1.Normals[2]));
    //    }

    //    if (aabb1.Points != null)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[0]), math.Vector3(aabb1.Points[1]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[1]), math.Vector3(aabb1.Points[2]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[2]), math.Vector3(aabb1.Points[3]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[3]), math.Vector3(aabb1.Points[0]));

    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[4]), math.Vector3(aabb1.Points[5]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[5]), math.Vector3(aabb1.Points[6]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[6]), math.Vector3(aabb1.Points[7]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Points[7]), math.Vector3(aabb1.Points[4]));

    //        Gizmos.DrawLine(math.Vector3(aabb1.Center), math.Vector3(aabb1.Center + 10 * aabb1.Normals[0]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Center), math.Vector3(aabb1.Center + 10 * aabb1.Normals[1]));
    //        Gizmos.DrawLine(math.Vector3(aabb1.Center), math.Vector3(aabb1.Center + 10 * aabb1.Normals[2]));

    //    }

    //}
    void Update()
    {
        aabb1.Update(math.fix3(AABB1.transform.position));
        obb1.Update(math.fix3(OBB1.transform.position), math.quaternion(OBB1.transform.rotation));
        gos[AABB1] = aabb1;
        gos[OBB1] = obb1;
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
