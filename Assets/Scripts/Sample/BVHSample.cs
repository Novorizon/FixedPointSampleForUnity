using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using UnityEngine.Profiling;

public class BVHSample : MonoBehaviour
{
    GameObject AABB1;
    GameObject AABB2;
    GameObject OBB1;
    GameObject OBB2;
    GameObject Sphere1;
    GameObject Sphere2;

    AABB aabb1;
    AABB aabb2;
    OBB obb1;
    OBB obb2;
    Sphere sphere1;
    Sphere sphere2;
    GameObject AABB3;
    GameObject AABB4;
    AABB aabb3;
    AABB aabb4;
    Node a;
    Color[] colors = new Color[6] { Color.red, Color.blue, Color.green, Color.black, Color.grey, Color.yellow };
    int count = 0;
    void Start()
    {
        AABB1 = GameObject.Find("AABB1");
        AABB2 = GameObject.Find("AABB2");
        OBB1 = GameObject.Find("OBB1");
        OBB2 = GameObject.Find("OBB2");
        Sphere1 = GameObject.Find("Sphere1");
        Sphere2 = GameObject.Find("Sphere2");

        aabb1 = new AABB(math.fix3(AABB1.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB1.GetComponent<BoxCollider>().bounds.max));
        aabb2 = new AABB(math.fix3(AABB2.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB2.GetComponent<BoxCollider>().bounds.max));
        obb1 = new OBB(math.fix3(OBB1.GetComponent<BoxCollider>().bounds.min), math.fix3(OBB1.GetComponent<BoxCollider>().bounds.max));
        obb2 = new OBB(math.fix3(OBB2.GetComponent<BoxCollider>().bounds.min), math.fix3(OBB2.GetComponent<BoxCollider>().bounds.max));
        sphere1 = new Sphere(math.fix3(Sphere1.transform.position), Sphere1.GetComponent<SphereCollider>().radius);
        sphere2 = new Sphere(math.fix3(Sphere2.transform.position), Sphere2.GetComponent<SphereCollider>().radius * 2);

        AABB3 = GameObject.Find("AABB3");
        AABB4 = GameObject.Find("AABB4");

        aabb3 = new AABB(math.fix3(AABB3.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB3.GetComponent<BoxCollider>().bounds.max));
        aabb4 = new AABB(math.fix3(AABB4.GetComponent<BoxCollider>().bounds.min), math.fix3(AABB4.GetComponent<BoxCollider>().bounds.max));
        BVHTree.Init(1024);
    }


    private void Draw(Node node)
    {

        if (node == null)
            return;
        count++;
        if (node != null)
        {
            Gizmos.color = colors[count % 6];
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[0]), math.Vector3(node.aabb.Points[1]));
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[1]), math.Vector3(node.aabb.Points[2]));
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[2]), math.Vector3(node.aabb.Points[3]));
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[3]), math.Vector3(node.aabb.Points[0]));

            Gizmos.DrawLine(math.Vector3(node.aabb.Points[4]), math.Vector3(node.aabb.Points[5]));
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[5]), math.Vector3(node.aabb.Points[6]));
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[6]), math.Vector3(node.aabb.Points[7]));
            Gizmos.DrawLine(math.Vector3(node.aabb.Points[7]), math.Vector3(node.aabb.Points[4]));

            Draw(node.left);
            Draw(node.right);




        }
    }
    private void OnDrawGizmos()
    {
        count = 0;
        Draw(a);

    }
    void Update()
    {
        aabb1.Update(math.fix3(AABB1.transform.position));
        aabb2.Update(math.fix3(AABB2.transform.position));
        obb1.Update(math.fix3(OBB1.transform.position), math.quaternion(OBB1.transform.rotation));
        obb2.Update(math.fix3(OBB2.transform.position), math.quaternion(OBB2.transform.rotation));
        sphere1.Update(math.fix3(Sphere1.transform.position));
        sphere2.Update(math.fix3(Sphere2.transform.position));
        aabb3.Update(math.fix3(AABB3.transform.position));
        aabb4.Update(math.fix3(AABB4.transform.position));

        //Node.nodes.Clear();
        //Node.count=0;
        //Node[] list = new Node[4] { new Node(aabb1.Bounds), new Node(aabb2.Bounds), new Node(obb1.Bounds), new Node(obb2.Bounds) };
        AABB[] list = new AABB[6] { aabb1.Bounds, aabb2.Bounds, obb1.Bounds, obb2.Bounds, sphere1.Bounds, sphere2.Bounds };
        //Node[] list = new Node[6] { new Node(aabb1.Bounds), new Node(aabb2.Bounds), new Node(obb1.Bounds), new Node(obb2.Bounds), new Node(aabb3.Bounds), new Node(aabb4.Bounds) };

        List<AABB> aaa = new List<AABB>();

        int length = 1000;
        for (int i = 0; i < length; i++)
        {
            aaa.Add(list[i % 6]);
        }
        list = aaa.ToArray();

        AABB[] list1 = new AABB[length];
        for (int i = 0; i < length; i++)
        {
            list1[i] = new AABB(list[i].Min + fix3.one * i, list[i].Max + fix3.one * i);
        }

        Profiler.BeginSample("BuildTree");
        BVHTree.BuildTree(list, length);
        Profiler.EndSample();
        a = BVHTree.Root;

        int count = 1000;
        Profiler.BeginSample("Hit");
        Mathematica.Physics.RaycastHit hit = new Mathematica.Physics.RaycastHit();
        for (int i = 0; i < count; i++)
        {
            //BVHTree.Hit(aabb3, hit);
        }
        Profiler.EndSample();
        Profiler.BeginSample("IsOverlap");
        for (int i = 0; i < count; i++)
        {
            if (Geometry.IsOverlap(aabb3, aabb1)) ;

        }
        Profiler.EndSample();

        Debug.LogError(Node.count);













        if (Geometry.IsOverlap(aabb3, aabb1))
        {
            AABB3.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            AABB3.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
}
