using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActions : MonoBehaviour
{
    public SceneMode mode;

    [Header("Token spawning")]
    public Spawner spawner;
    public TokenKey tokenKeyL;
    public TokenKey tokenKeyR;

    [Header("Controller state")]
    public ControllerState stateL;
    [Range(0f, 1f)]
    public float chargeL = 0f;
    public ControllerState stateR;
     [Range(0f, 1f)]
    public float chargeR = 0f;
    
    [Header("Input rigs")]
    public InputRig inputRigVR;
    public InputRig inputRigMockup;
    private InputRig _rig;
    private float _chargeSpeed;
    private float _defaultCharge;

    void Start()
    {
        if(SceneModeHandler.instance)
        {
            mode = SceneModeHandler.instance.mode;
        }
        if(mode == SceneMode.VR)
        {
            _rig = inputRigVR;
        }
        else
        {
            _rig = inputRigMockup;
        }

        _chargeSpeed = Config.active.chargeSpeedFactor;
        _defaultCharge = Config.active.defaultCharge;
    }

    void Update()
    {
        UpdateLeftController();
    }

    private void UpdateLeftController() {
        switch(stateL) {
            case ControllerState.Idle:
                if(_rig.pressedL)
                {
                    chargeL = _defaultCharge;
                    stateL = ControllerState.Charging;
                }
                break;
            case ControllerState.Charging:
                chargeL += Time.deltaTime * _chargeSpeed;
                if(chargeL >= 1f) {
                    chargeL = 1f;
                    stateL = ControllerState.Fire;
                }
                else if(!_rig.pressedL) {
                    stateL = ControllerState.Fire;
                }
                break;
            case ControllerState.Fire:
                spawner.SpawnToken(
                    _rig.originL,
                    tokenKeyL,
                    chargeL
                );
                chargeL = 0f;
                stateL = ControllerState.Idle;
                break;
            default:
                break;
        }
    }
}

public enum ControllerState
{
    Idle,
    Charging,
    Fire
}
