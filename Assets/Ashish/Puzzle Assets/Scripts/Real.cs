    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Real : MonoBehaviour
{
    private string Color;
    private Material mat;
    Collider realCol;
    bool isCollided = false;
    Transform targetTransform;
    Quaternion targetRotation;

    private void Start()
    {
        realCol = GetComponent<Collider>();
        mat = GetComponent<Renderer>().material;
        Color = mat.name;
    }
    private void Update()
    {
        if(isCollided)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        need need = collision.gameObject.GetComponent<need>();
        if (CompareTag(collision.gameObject.tag) && (need.GetColor() == this.Color))
        {
            isCollided = true;
            targetTransform.position = collision.gameObject.transform.position;
            targetRotation = collision.gameObject.transform.rotation;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            RemoveElements(collision, rb);
        }
    }

    private void RemoveElements(Collision collision, Rigidbody rb)
    {
        //Destroy(rb);
        Destroy(collision.gameObject);
        Destroy(realCol);
    }
}
