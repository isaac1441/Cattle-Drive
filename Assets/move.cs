using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class move : MonoBehaviour
{

    public float speed = 5f;
    private Vector3 target;
    private bool selected;
    public static List<move> moveableObjects = new List<move>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveableObjects.Add(this);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && selected)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        selected = true;

        foreach(move obj in moveableObjects)
        {
            if(obj != this)
            {
                obj.selected = false;
            }
        }
    }
}
