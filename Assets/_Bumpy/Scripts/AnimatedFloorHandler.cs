using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatedFloorHandler : MonoBehaviour
{
    public AnimFloorState state;
    public float raisedDuration = 12f;
    public float loweredY = -10f;
    public float animSpeed = 10f;
    
    private AnimatedFloor _animatedFloor;
    private float _targetY;
    private Dictionary<AnimFloorState, Action> _updateActions;
    private Vector3 _position;
    private Dictionary<GraphFunctionName, GraphFunctionName> _nextFuntion;
    private bool _hasSwitchedFunction;
    private bool _isNextSwitchInvoked;

    void Start()
    {
        _animatedFloor = GetComponent<AnimatedFloor>();
        _targetY = transform.position.y;

        _updateActions = new Dictionary<AnimFloorState, Action>() {
            { AnimFloorState.Lowering, LoweringUpdate },
            { AnimFloorState.Lowered, LoweredUpdate },
            { AnimFloorState.Raising, RaisingUpdate },
            { AnimFloorState.Raised, RaisedUpdate }
        };

        _nextFuntion = new Dictionary<GraphFunctionName, GraphFunctionName>() {
            { GraphFunctionName.Sine, GraphFunctionName.Sine2D },
            { GraphFunctionName.Sine2D, GraphFunctionName.Chaotic },
            { GraphFunctionName.Chaotic, GraphFunctionName.Sine }
        };

        _position = new Vector3(0f, loweredY, 0f);
        transform.position = _position;
        state = AnimFloorState.Raising;
    }

    // Update is called once per frame
    void Update()
    {
        _updateActions[state]();
    }

    private void LoweringUpdate()
    {
        if(_position.y > loweredY)
        {
            _position.Set(0f, _position.y -= animSpeed * Time.deltaTime, 0f);
            transform.position = _position;
        }
        else
        {
            _position.Set(0f, loweredY, 0f);
            transform.position = _position;
            _hasSwitchedFunction = false;
            state = AnimFloorState.Lowered;
        }
    }

    private void LoweredUpdate()
    {
        _animatedFloor.functionSelector = _nextFuntion[_animatedFloor.functionSelector];
        state = AnimFloorState.Raising;
    }

    private void RaisingUpdate()
    {
        if(_position.y < _targetY)
        {
            _position.Set(0f, _position.y += animSpeed * Time.deltaTime, 0f);
            transform.position = _position;
        }
        else
        {
            _position.Set(0f, _targetY, 0f);
            transform.position = _position;
            _isNextSwitchInvoked = false;
            state = AnimFloorState.Raised;
        }
    }

    private void RaisedUpdate()
    {
        if(!_isNextSwitchInvoked) {
            Invoke("TriggerLoweringState", raisedDuration);
            _isNextSwitchInvoked = true;
        }
    }

    private void TriggerLoweringState() {
        state = AnimFloorState.Lowering;
    }
}

public enum AnimFloorState {
    Lowering,
    Lowered,
    Raising,
    Raised
}
