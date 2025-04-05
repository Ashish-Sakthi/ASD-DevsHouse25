using UnityEngine;

public class ImageEnabler : MonoBehaviour
{
    public GameObject[] imagesToShow; // Assign your UI Images here

    public void ShowImages()
    {
        foreach (GameObject img in imagesToShow)
        {
            if (img != null)
                img.SetActive(true);
        }
    }

    // Method to enable a specific GameObject
    public void EnableGameObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(true);
        }
    }
}
