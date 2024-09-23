using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {

        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {

        }
        else if (Input.GetKey(KeyCode.D))
        {
            
        }
    }
}
