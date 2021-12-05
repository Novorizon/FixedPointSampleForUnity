using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using UnityEngine.Profiling;

public class BVHSample2: MonoBehaviour
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
            gos.Add(go);

        }
        //AABB aabb = new AABB(math.fix3(go.GetComponent<SphereCollider>().bounds.min), math.fix3(go.GetComponent<SphereCollider>().bounds.max));
        //Rectangle a = new Rectangle(aabb.Min.x, aabb.Min.y, aabb.Min.z, aabb.Max.x, aabb.Max.y, aabb.Max.z);
        AABB3 = GameObject.Find("AABB3");
        aabb3 = new AABB(math.fix3(AABB3.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB3.GetComponent<BoxCollider>().bounds.max));
        BVHTree.Init(1024);
    }


    private void Draw(Node node)
    {

    }
    private void OnDrawGizmos()
    {
    }
    void Update()
    {
        //Span<AABB> list = new Span<AABB>();

        AABB[] list = new AABB[length];
        for (int i = 0; i < gos.Count; i++)
        {
            list[i] = new AABB(math.fix3(gos[i].GetComponent<SphereCollider>().bounds.min), math.fix3(gos[i].GetComponent<SphereCollider>().bounds.max));
        }

        Profiler.BeginSample("BuildTree");
        BVHTree.BuildTree(list, length);
        Profiler.EndSample();






        aabb3.Update(math.fix3(AABB3.transform.position));
        fix3 p = aabb3.Center;
        Profiler.BeginSample("Hit");
        Mathematica.Physics.RaycastHit hit = new Mathematica.Physics.RaycastHit();
        for (int i = 0; i < length; i++)
        {
            BVHTree.Hit(p, hit);
        }
        Profiler.EndSample();


        Debug.LogError(Node.count);













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
