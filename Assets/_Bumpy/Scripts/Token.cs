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
    }

    public void setState(TokenState targetState) {
        _exitStateActions[state]();
        _enterStateActions[targetState]();
        state = targetState;
    }

    private void EnterStandby() 
    {
        gameObject.SetActive(false);
    }

    private void EnterActivate()
    {

    }

    private void EnterActive()
    {

    }

    private void ExitStandby()
    {
        gameObject.SetActive(true);
    }

    private void ExitActivate()
    {

    }

    private void ExitActive()
    {

    }

    private void UpdateStandby()
    {

    }

    private void UpdateActivate()
    {

    }

    private void UpdateActive()
    {

    }
}

public enum TokenState
{
    Standby,
    Activate,
    Active
}
