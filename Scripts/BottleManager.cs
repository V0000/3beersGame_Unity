using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleManager : MonoBehaviour
{
    [Range(0, 1)]
    public float fullness = 1f;
    public bool isflows = false;
    public GameObject stream;
    public GameObject beer;
    Renderer BeerRend;
    void Start()
    {
        BeerRend = beer.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        stream.SetActive(isflows);
        BeerRend.material.SetFloat("_FillAmount", fullness);
    }
}
