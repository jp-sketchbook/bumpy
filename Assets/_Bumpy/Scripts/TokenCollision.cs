using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenCollision : MonoBehaviour
{
    private TokenAudio _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = transform.parent.GetComponent<TokenAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision! with " + collision.gameObject.tag);
        _audio.PlayCollisionClip(collision.gameObject.tag, collision.contacts[0].point);
    }
}
