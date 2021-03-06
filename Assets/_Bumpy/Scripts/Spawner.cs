﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TokenPool CubeTokensPool;
    public TokenPool SphereTokenPool;

    private Dictionary<TokenKey, TokenPool> _pools;

    void Start()
    {
        Init();
    }

    private void Init() {
        _pools = new Dictionary<TokenKey, TokenPool>() {
            { TokenKey.CubeToken, CubeTokensPool },
            { TokenKey.SphereToken, SphereTokenPool }
        };
    }

    public void SpawnToken(Transform origin, TokenKey key)
    {
        if(_pools[key] != null) {
            var token = _pools[key].GetToken();
            token.transform.position = origin.position;
            token.transform.rotation = origin.rotation;
            token.SetState(TokenState.Activate);
        }
    }

    public void SpawnToken(Transform origin, TokenKey key, float charge)
    {
        if(_pools[key] != null) {
            var token = _pools[key].GetToken();
            token.transform.position = origin.position;
            token.transform.rotation = origin.rotation;
            token.Fire(charge);
        }
    }

    public void ResetPools()
    {
        _pools[TokenKey.CubeToken].Reset();
        _pools[TokenKey.SphereToken].Reset();
    }
}
