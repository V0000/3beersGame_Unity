using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    private float sizeOfpuddle = 0.1f;
    private float scalingSpeed = 0.0003f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < sizeOfpuddle)
        {
            transform.localScale += new Vector3(scalingSpeed, 0f, scalingSpeed);
        }
    }
}
