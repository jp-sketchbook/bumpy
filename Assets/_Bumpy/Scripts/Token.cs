using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Token : MonoBehaviour
{
    public TokenState state;

    private Dictionary<TokenState, Action> _enterStateActions;
    private Dictionary<TokenState, Action> _exitStateActions;
    private Dictionary<TokenState, Action> _updateActions;

    private Rigidbody _body;
    private float _maxForce;
    private float _force;
    private Vector3 _scale;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        SetState(TokenState.Standby);
    }

    // Update is called once per frame
    void Update()
    {
        _updateActions[state]();
    }

    private void Init()
    {
        _enterStateActions = new Dictionary<TokenState, Action>() {
            { TokenState.Standby, EnterStandby },
            { TokenState.Activate, EnterActivate },
            { TokenState.Active, EnterActive }
        };
        _exitStateActions = new Dictionary<TokenState, Action>() {
            { TokenState.Standby, ExitStandby },
            { TokenState.Activate, ExitActivate },
            { TokenState.Active, ExitActive }
        };
        _updateActions = new Dictionary<TokenState, Action>() {
            { TokenState.Standby, UpdateStandby },
            { TokenState.Activate, UpdateActivate },
            { TokenState.Active, UpdateActive }
        };
        _body = GetComponent<Rigidbody>();
        _maxForce = Config.active.tokenMaxForce;
        _force = _maxForce * 0.5f;
        _scale = new Vector3();
        SetScale(Config.active.tokenScale);
    }

    // Charge range 0f to 1f
    public void Fire(float charge)
    {
        _force = _maxForce * charge;
        SetState(TokenState.Activate);
    }

    public void SetState(TokenState targetState) {
        _exitStateActions[state]();
        _enterStateActions[targetState]();
        state = targetState;
    }

    // STANDBY
    private void EnterStandby() 
    {
        gameObject.SetActive(false);
    }

    private void UpdateStandby()
    {
        return;
    }

    private void ExitStandby()
    {
        gameObject.SetActive(true);
        ResetBodyVelocity();
    }

    // ACTIVATE
    private void EnterActivate()
    {
        _body.AddForce(transform.forward * _force, ForceMode.Impulse);
        SetScale(0.01f);
    }

    private void UpdateActivate()
    {
        var currentScale = transform.localScale.x;
        if(currentScale < Config.active.tokenScale)
        {
            SetScale(currentScale + Config.active.tokenGrowthFactor * Time.deltaTime);
        }
        else {
            SetState(TokenState.Active);
        }
    }

    private void ExitActivate()
    {
        SetScale(Config.active.tokenScale);
    }

    // ACTIVE
    private void EnterActive()
    {

    }
    private void UpdateActive()
    {

    }

    private void ExitActive()
    {
        ResetBodyVelocity();
    }

    // UTIL
    private void ResetBodyVelocity() 
    {
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
    }

    private void SetScale(float scaleFactor)
    {
        _scale.Set(scaleFactor, scaleFactor, scaleFactor);
        transform.localScale = _scale;
    }
}

public enum TokenState
{
    Standby,
    Activate,
    Active
}
