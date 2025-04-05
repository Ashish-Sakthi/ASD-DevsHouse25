using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GlobalSettingsManager : MonoBehaviour
{
    public static GlobalSettingsManager Instance;

    [Header("Lighting Settings")]
    public Light globalLight;

    [Header("Audio Settings")]
    public AudioMixer audioMixer; // Create an AudioMixer and assign here

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        ApplySavedSettings();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySavedSettings();
    }

    public void SetLight(bool isOn)
    {
        if (globalLight != null)
        {
            globalLight.enabled = isOn;
        }

        PlayerPrefs.SetInt("LightSetting", isOn ? 1 : 0);
    }

    public void SetSound(bool isOn)
    {
        audioMixer.SetFloat("MasterVolume", isOn ? 0 : -80); // 0 dB for on, -80 dB for off
        PlayerPrefs.SetInt("SoundSetting", isOn ? 1 : 0);
    }

    void ApplySavedSettings()
    {
        bool isLightOn = PlayerPrefs.GetInt("LightSetting", 1) == 1;
        bool isSoundOn = PlayerPrefs.GetInt("SoundSetting", 1) == 1;

        if (globalLight != null)
            globalLight.enabled = isLightOn;

        audioMixer.SetFloat("MasterVolume", isSoundOn ? 0 : -80);
    }
}
