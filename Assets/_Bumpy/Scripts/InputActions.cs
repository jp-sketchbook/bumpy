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
    public float chargeScaleMultiplier = 0.1f; // Scales visual representation of charge amount
    private Vector3 _chargeLScale;
    private Vector3 _chargeRScale;
    
    [Header("Input rigs")]
    public InputRig inputRigVR;
    public InputRig inputRigMockup;
    private InputRig _rig;
    private float _chargeSpeed;
    private float _defaultCharge;

    void Start()
    {
        _chargeLScale = new Vector3();
        _chargeRScale = new Vector3();
        
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

        _rig.SetOnResetAction(spawner.ResetPools);
    }

    void Update()
    {
        UpdateLeftController();
        UpdateRightController();
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
                var visualScaleFactor = chargeL * chargeScaleMultiplier;
                _chargeLScale.Set(visualScaleFactor, visualScaleFactor, visualScaleFactor);
                _rig.originL.localScale = _chargeLScale;
                
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
                _rig.originL.localScale = Vector3.zero;
                stateL = ControllerState.Idle;
                break;
            default:
                break;
        }
    }

    private void UpdateRightController() {
        switch(stateR) {
            case ControllerState.Idle:
                if(_rig.pressedR)
                {
                    chargeR = _defaultCharge;
                    stateR = ControllerState.Charging;
                }
                break;
            case ControllerState.Charging:
                var visualScaleFactor = chargeR * chargeScaleMultiplier;
                _chargeRScale.Set(visualScaleFactor, visualScaleFactor, visualScaleFactor);
                _rig.originR.localScale = _chargeRScale;

                chargeR += Time.deltaTime * _chargeSpeed;
                if(chargeR >= 1f) {
                    chargeR = 1f;
                    stateR = ControllerState.Fire;
                }
                else if(!_rig.pressedR) {
                    stateR = ControllerState.Fire;
                }
                break;
            case ControllerState.Fire:
                spawner.SpawnToken(
                    _rig.originR,
                    tokenKeyR,
                    chargeR
                );
                chargeR = 0f;
                _rig.originR.localScale = Vector3.zero;
                stateR = ControllerState.Idle;
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
