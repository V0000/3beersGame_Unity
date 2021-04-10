using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    private Vector3 startPos = new Vector3(-1f, -2f, 4f);
    private Vector3 finalPos = new Vector3(-1f, 2.5f, 4f);
    private float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, finalPos, speed);
    }
}
