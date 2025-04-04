using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinManager : MonoBehaviour
{
    public enum BinType
    {
        Fruits,
        Vegetables
    }

    public BinType binType;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null) return;

        if (other.CompareTag("Fruit"))
        {
            if (binType == BinType.Fruits)
            {
                Destroy(other.gameObject);
            }
            else
            {
                StartCoroutine(ReturnToOriginalPosition(item));
            }
        }
        else if (other.CompareTag("Vegetable"))
        {
            if (binType == BinType.Vegetables)
            {
                Destroy(other.gameObject);
            }
            else
            {
                StartCoroutine(ReturnToOriginalPosition(item));
            }
        }
    }

    private IEnumerator ReturnToOriginalPosition(Item item)
    {
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        Vector3 startPosition = item.transform.position;
        Quaternion startRotation = item.transform.rotation;

        while (elapsedTime < duration)
        {
            item.transform.position = Vector3.Lerp(startPosition, item.OriginalPosition, elapsedTime / duration);
            item.transform.rotation = Quaternion.Slerp(startRotation, item.OriginalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        item.transform.position = item.OriginalPosition;
        item.transform.rotation = item.OriginalRotation;
    }
}
