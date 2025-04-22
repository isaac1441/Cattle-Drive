using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camleft : MonoBehaviour {
    public float speed = 1f;
    void Start ()
    {

    }
    void Update ()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
    
    
}
