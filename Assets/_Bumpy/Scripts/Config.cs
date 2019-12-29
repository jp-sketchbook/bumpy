using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    [Range(0.05f, 0.5f)]
    public float tokenScale = 0.25f;
    [Range(0.05f, 1.5f)]
    public float tokenGrowthFactor = 1f;
    [Range(1f, 15f)]
    public float tokenForce = 5f;
    [Range(10, 100)]
    public int poolCapacity = 10;

    public static Config active;

    private void Awake() {
        if(!active)
        {
            active = this;
        }
        else
        {
            Destroy(this);
        }
    }
}    
