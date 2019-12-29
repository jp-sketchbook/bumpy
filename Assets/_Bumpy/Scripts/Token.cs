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
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        setState(TokenState.Standby);
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
    }

    public void setState(TokenState targetState) {
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
        _body.velocity = Vector3.zero;
        _body.angularVelocity = Vector3.zero;
    }

    // ACTIVATE
    private void EnterActivate()
    {
        
    }

    private void UpdateActivate()
    {

    }

    private void ExitActivate()
    {

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

    }
}

public enum TokenState
{
    Standby,
    Activate,
    Active
}
