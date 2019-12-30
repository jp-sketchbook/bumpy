using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputRig : MonoBehaviour
{   
    public SceneMode mode;
    public Transform originL;
    public Transform originR;
    public bool pressedL;
    public bool pressedR;

    private Action _update;
    private Action _onReset;

    void Start()
    {
        if(mode == SceneMode.VR) {
            _update = VRUpdate;
        }
        else
        {
            _update = MockupUpdate;
        }
    }

    void Update()
    {
        _update();
    }

    private void MockupUpdate()
    {
        pressedL = Input.GetMouseButton(0);
        pressedR = Input.GetMouseButton(1);
        if(Input.GetMouseButtonDown(2)) {
            if(_onReset != null)
            {
                _onReset();
            }
        }
    }

    private void VRUpdate()
    {
        pressedL = OVRInput.Get(OVRInput.RawButton.LIndexTrigger);
        pressedR = OVRInput.Get(OVRInput.RawButton.RIndexTrigger);
        if(OVRInput.Get(OVRInput.Button.One)) {
            if(_onReset != null) {
                _onReset();
            }
        }
    }

    public void SetOnResetAction(Action action)
    {
        _onReset = action;
    }
}
