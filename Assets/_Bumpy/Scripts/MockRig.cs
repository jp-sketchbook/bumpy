using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockRig : MonoBehaviour
{
    public Spawner spawner;
    public Transform leftOrigin;
    public TokenKey leftKey;
    public Transform rightOrigin;
    public TokenKey rightKey;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Primary mouse click.");
            spawner.SpawnToken(leftOrigin, leftKey);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Secondary mouse click.");
            spawner.SpawnToken(rightOrigin, rightKey);
        }
    }
}
