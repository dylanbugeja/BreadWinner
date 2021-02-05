using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookg : MonoBehaviour
{
    GrapplingHook grappling;
    DistanceJoint2D joint2D;

    // Start is called before the first frame update
    void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<GrapplingHook>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(""))
        {
            joint2D.enabled = true;
            grappling.isAttach = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
