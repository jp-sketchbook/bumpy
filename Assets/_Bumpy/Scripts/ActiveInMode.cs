using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInMode : MonoBehaviour
{
    public SceneMode activeMode;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneModeHandler.instance)
        {
            gameObject.SetActive(SceneModeHandler.instance.mode == activeMode);
        }
        else
        {
            Debug.LogError("Could not retreive scene mode!");
        }
    }
}
