using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to switch scenes
    public void SwitchScene(string sceneName)
    {
        Debug.Log("SwitchScene called with sceneName: " + sceneName);
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Please check if the scene is added to the build settings.");
        }
    }
}
