using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using UnityEngine.Profiling;
using Rectangle = Slb.AvocetVM.Utilities.RTree.Rectangle;
using Slb.AvocetVM.Utilities.RTree;

public class BVHSample1 : MonoBehaviour
{
    List<GameObject> gos;
    GameObject AABB3;
    AABB aabb3;
    int length = 1000;
    void Start()
    {
        gos = new List<GameObject>();
        for (int i = 0; i < length; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = UnityEngine.Random.insideUnitSphere * 100;
            go.name = "Sphere " + i;
            gos.Add(go);

        }
        //AABB aabb = new AABB(math.fix3(go.GetComponent<SphereCollider>().bounds.min), math.fix3(go.GetComponent<SphereCollider>().bounds.max));
        //Rectangle a = new Rectangle(aabb.Min.x, aabb.Min.y, aabb.Min.z, aabb.Max.x, aabb.Max.y, aabb.Max.z);
        AABB3 = GameObject.Find("AABB3");
        aabb3 = new AABB(math.fix3(AABB3.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB3.GetComponent<BoxCollider>().bounds.max));

    }


    private void Draw(Node node)
    {

    }
    private void OnDrawGizmos()
    {
    }
    void Update()
    {
        aabb3.Update(math.fix3(AABB3.transform.position));
        Rectangle r = new Rectangle(aabb3.Min.x, aabb3.Min.y, aabb3.Min.z, aabb3.Max.x, aabb3.Max.y, aabb3.Max.z);

        RTree<int> tree = new RTree<int>();
        List<Rectangle> rs = new List<Rectangle>();
        for (int i = 0; i < gos.Count; i++)
        {
            AABB aabb = new AABB(math.fix3(gos[i].GetComponent<SphereCollider>().bounds.min), math.fix3(gos[i].GetComponent<SphereCollider>().bounds.max));
            Rectangle a = new Rectangle(aabb.Min.x, aabb.Min.y, aabb.Min.z, aabb.Max.x, aabb.Max.y, aabb.Max.z);
            rs.Add(a);
        }

        Profiler.BeginSample("RTREE");
        for (int i = 0; i < rs.Count; i++)
        {
            tree.Add(rs[i], i);
        }
        Profiler.EndSample();



        List<int> aa = new List<int>();
        Profiler.BeginSample("Intersects");
        for (int i = 0; i < 1000; i++)
        {
            aa = tree.Intersects(r);
        }
        Profiler.EndSample();
        if (aa.Count > 0)
        {
            for (int i = 0; i < aa.Count; i++)
            {
                Debug.LogError(aa[i]);
            }
            Debug.LogError("=============");
        }

















        //if (Geometry.IsOverlap(aabb3, aabb1))
        //{
        //    AABB3.GetComponent<MeshRenderer>().material.color = Color.red;
        //}
        //else
        //{
        //    AABB3.GetComponent<MeshRenderer>().material.color = Color.yellow;
        //}
    }
}
