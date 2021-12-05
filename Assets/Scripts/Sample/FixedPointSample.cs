using Mathematica;
using System.Collections.Generic;
using UnityEngine;
using math = Mathematica.math;
using quaternion = Mathematica.quaternion;
using Bounds = Mathematica.Bounds;
using Physics = Mathematica.Physics.Physics;

public class FixedPointSample : MonoBehaviour
{
    // Start is called before the first frame update
    fix3 position;
    quaternion quaternion;
    List<GameObject> gameObjects;
    Dictionary<GameObject, Bounds> gos = new Dictionary<GameObject, Bounds>();
    Dictionary<GameObject, AABB> aabbs=new Dictionary<GameObject, AABB>();
    Dictionary<GameObject, OBB> obbs = new Dictionary<GameObject, OBB>();
    Dictionary<GameObject, Sphere> spheres = new Dictionary<GameObject, Sphere>();
    Dictionary<GameObject, Capsule> capsules = new Dictionary<GameObject, Capsule>();
    int length = 10;
    void Start()
    {
    }
    static float speed = 10;
    // Update is called once per frame
    void FixedUpdate()
    {
        var go = gos.GetEnumerator();
        while (go.MoveNext())
        {
            var Current = go.Current;
            quaternion r = quaternion.AxisAngle(math.fix3(Current.Key.transform.forward), speed * Time.fixedDeltaTime);
            fix3 center = r * math.fix3(Current.Key.transform.position);
            Current.Key.transform.position = math.Vector3(center);
            if (Current.Value is AABB aabb)
            {
                aabb.Update(center);
            }
            if (Current.Value is OBB obb)
            {
                obb.Update(center, r);
            }
            if (Current.Value is Sphere sphere)
            {
                sphere.Update(center);
            }
            if (Current.Value is Capsule capsule)
            {
                capsule.Update(center, r);
            }

        }
        var     go1 = gos.GetEnumerator();
        while (go1.MoveNext())
        {
            var Current1 = go1.Current;
            var go2 = gos.GetEnumerator();
            while (go2.MoveNext())
            {
                var Current2 = go2.Current;
                if(Current1.Key!=Current2.Key)
                {
                    if (Physics.IsOverlap(Current1.Value, Current2.Value))
                    {
                        Current1.Key.GetComponent<MeshRenderer>().material.color = Color.red;
                    }

                }
            }
        }
        fix3 right = math.normalize(math.cross(position, fix3.up));
        fix3 foward = math.normalize(math.cross(position, right));

        quaternion = quaternion.AxisAngle(foward, speed * Time.fixedDeltaTime);
        position = quaternion * position;

        gameObject.transform.position = math.Vector3(position);
        gameObject.transform.rotation = math.Quaternion(quaternion);


    }
}
