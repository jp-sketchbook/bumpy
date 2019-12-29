using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenPool : MonoBehaviour
{
    public GameObject tokenPrefab;
    public int capacity;
    private Queue<Token> _tokens;
    
    void Start()
    {
        _tokens = new Queue<Token>();
        for (int i = 0; i < capacity; i++)
        {
            var token = Instantiate(tokenPrefab).GetComponent<Token>();
            token.transform.parent = transform;
            _tokens.Enqueue(token);
        }
    }

    public Token GetToken() {
        var token = _tokens.Dequeue();
        _tokens.Enqueue(token);
        return token;
    }
}
