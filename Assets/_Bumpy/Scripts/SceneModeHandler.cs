using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneModeHandler : MonoBehaviour
{
    public SceneMode mode;

    public static SceneModeHandler instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
