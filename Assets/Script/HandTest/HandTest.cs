using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTest : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter is run");
        if (other.gameObject.tag == "Test")
        {
            Debug.Log("EnterColor is run");
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit is run");
        if (other.gameObject.tag == "Test")
        {
            Debug.Log("ExitColor is run");
            other.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
