using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRInputActions : MonoBehaviour
{
    public Spawner spawner;
    public Transform leftOrigin;
    public TokenKey leftKey;
    public Transform rightOrigin;
    public TokenKey rightKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            triggerLeft();
        }
        if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            triggerRight();
        }
    }

    private void triggerLeft()
    {
        spawner.SpawnToken(leftOrigin, leftKey);
    }

    private void triggerRight()
    {
        spawner.SpawnToken(rightOrigin, rightKey);
    }
}
