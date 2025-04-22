using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class repbg : MonoBehaviour
{
    Vector3 startPos;
    float repW;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        repW = GetComponent<BoxCollider2D>().size.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPos.x -  repW) {
            transform.position = startPos;
        }
    }
}
