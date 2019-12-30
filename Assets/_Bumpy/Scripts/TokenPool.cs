using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TokenAudio))]
public class TokenPool : MonoBehaviour
{
    public GameObject tokenPrefab;
    private Queue<Token> _tokens;
    
    void Start()
    {
        _tokens = new Queue<Token>();
        for (int i = 0; i < Config.active.poolCapacity; i++)
        {
            var token = Instantiate(tokenPrefab).GetComponent<Token>();
            token.transform.parent = transform;
            _tokens.Enqueue(token);
        }
    }

    public Token GetToken()
    {
        var token = _tokens.Dequeue();
        _tokens.Enqueue(token);
        return token;
    }

    public void Reset()
    {
        foreach(var token in _tokens)
        {
            token.SetState(TokenState.Standby);
        }
    }
}
