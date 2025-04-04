using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 OriginalPosition { get; private set; }
    public Quaternion OriginalRotation { get; private set; }

    void Start()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
    }
}
