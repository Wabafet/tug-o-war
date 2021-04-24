using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Helper
{
    public static T Choose<T>(params T[] list)
    {
        return list[UnityEngine.Random.Range(0, list.Length)];
    }

    public static void Trace(params object[] list)
    {
        Debug.Log(string.Join(", ", list));
    }

    public static int Mod(int i, int m)
    {
        return (i % m + m) % m;
    }

    public static Rect RectFromTransform(RectTransform transform)
    {
        Rect rect = transform.rect;
        rect.position = rect.position + (Vector2)transform.position;
        return rect;
    }

    public static float EaseInOutQuart(float t)
    {
        if (t < 0.5f)
            return 8 * t * t * t * t;
        else
            return 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
    }

    public static Rect RectFromCentre(float x, float y, float width, float height) => new Rect(x - width * 0.5f, y - height * 0.5f, width, height);

    public static Vector2 PlanePoint2(Vector3 p) => new Vector2(p.x, p.z);

    public static Vector3 OnPlanePoint(Vector3 source, Vector3 target) => new Vector3(target.x, source.y, target.z);
    public static Vector3 PlanePoint(Vector3 p) => new Vector3(p.x, 0, p.z);

    public static float DistanceSquared(Vector3 a, Vector3 b) => Vector2.SqrMagnitude(PlanePoint2(b) - PlanePoint2(a));

    public static float VectorAngle(Vector3 a, Vector3 b) => Vector2.Angle(PlanePoint2(a), PlanePoint2(b));

    public static float Rand() => UnityEngine.Random.Range(-1, 1);
    public static float RandomSign() => UnityEngine.Random.value > 0.5f ? 1 : -1;

    public static Vector3 LeadTarget(Vector3 target, Vector3 velocity, Vector3 start, float speed, int iterations = 3)
    {
        Vector3 position = target;
        while (iterations-- > 0)
        {
            float t = Vector3.Magnitude(position - start) / speed;
            Vector3 p = target + velocity * t;
            float d = Vector3.SqrMagnitude(p - position);
            position = p;
            if (d < 1)
                break;
        }
        return position;
    }

    public static void SetLayerRecursively(GameObject gameObject, LayerMask layerMask)
    {
        gameObject.layer = layerMask;
        for (int i = 0; i < gameObject.transform.childCount; i++)
            SetLayerRecursively(gameObject.transform.GetChild(i).gameObject, layerMask);
    }
}
