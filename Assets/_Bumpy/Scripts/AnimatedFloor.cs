using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedFloor : MonoBehaviour
{
    public Transform segmentPrefab;
    public int resolution;
    public GraphFunctionName functionSelector;
    private Transform[] _segments;
    
    static private GraphFunction[] _graphFunctions = {
        SineFunction,
        Sine2DFunction,
        ChaoticFunction
    };

    private float _step;
    
    void Awake()
    {
        _segments = new Transform[resolution * resolution];
        
        _step = 8f / resolution;
        Vector3 scale = Vector3.one * _step;

        for (int i = 0; i < _segments.Length; i++)
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.localScale = scale;
            segment.SetParent(transform, false);
            _segments[i] = segment;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time;
        GraphFunction f = _graphFunctions[(int)functionSelector];
        for(int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * _step - 4f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * _step - 4f;
                _segments[i].localPosition = f(u, v, t);
            }
        }
    }

    const float pi = Mathf.PI;

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin((pi * (x + t)) / 6f) * 0.75f;
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin((pi * (x + t)) / 8f);
        p.y += Mathf.Sin((pi * (z + t)) / 8f);
        p.y *= 0.75f;
        p.z = z;
        return p;
    }

    static Vector3 ChaoticFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin((pi * (x + t)) / 1.5f);
        p.y += Mathf.Sin((pi * (z + t)) / 1.5f);
        p.y *= 0.5f;
        p.z = z;
        return p;
    }
}

public enum GraphFunctionName
{
    Sine,
    Sine2D,
    Chaotic
}

public delegate Vector3 GraphFunction (float u, float v, float t);
