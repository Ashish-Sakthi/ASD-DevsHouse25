using UnityEngine;

public class need : MonoBehaviour
{
    private string Colour;
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        Colour = mat.name;
    }

    public string GetColor() => Colour;


}
