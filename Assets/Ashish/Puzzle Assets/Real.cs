using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Real : MonoBehaviour
{
    private string Color;
    private Material mat;
    Collider realCol;

    private void Start()
    {
        realCol = GetComponent<Collider>();
        mat = GetComponent<Renderer>().material;
        Color = mat.name;
    }

    private void OnCollisionEnter(Collision collision)
    {
        need need =  collision.gameObject.GetComponent<need>();
        if(CompareTag(collision.gameObject.tag) && (need.GetColor() == this.Color))
        {
            gameObject.transform.position = collision.gameObject.transform.position;
            gameObject.transform.rotation = collision.gameObject.transform.rotation;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            RemoveElements(collision, rb);
        }
    }

    private void RemoveElements(Collision collision, Rigidbody rb)
    {
        Destroy(collision.gameObject);
        Destroy(realCol);
        Destroy(rb);
    }
}
