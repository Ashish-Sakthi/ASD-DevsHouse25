using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SensoryProfileControl : MonoBehaviour
{
    [Header("Post-Processing Volume")]
    public Volume sensoryVolume;

    [Header("UI Toggles")]
    public Toggle brightnessToggle;  // Brightness Control toggle
    public Toggle soundToggle;       // Sound Sensitivity toggle

    [Header("Post-Processing Effects")]
    private ColorAdjustments colorAdjustments;

    [Header("Audio Sources")]
    public AudioSource calmAudioSource;
    public AudioSource loudAudioSource;

    [Header("Singleton")]
    public static SensoryProfileControl Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        if (sensoryVolume == null)
        {
            Debug.LogError("Sensory Volume is not assigned.");
            return;
        }

        // Get Color Adjustments from volume
        if (sensoryVolume.profile.TryGet(out colorAdjustments))
        {
            // Initialize brightness toggle state (true = ON = ColorAdjustments disabled)
            brightnessToggle.isOn = !colorAdjustments.active;

            // Add listener
            brightnessToggle.onValueChanged.AddListener(ToggleBrightness);
        }
        else
        {
            Debug.LogError("Color Adjustments not found in Volume Profile.");
        }

        // Sound toggle listener
        if (calmAudioSource && loudAudioSource)
        {
            soundToggle.onValueChanged.AddListener(ToggleSoundSensitivity);
        }
    }

    private void ToggleBrightness(bool isOn)
    {
        // Toggle color adjustments (false = enabled, true = disabled)
        if (colorAdjustments != null)
        {
            colorAdjustments.active = !isOn;
        }
    }

    private void ToggleSoundSensitivity(bool isSensitive)
    {
        if (calmAudioSource && loudAudioSource)
        {
            if (isSensitive) // Loud sound
            {
                calmAudioSource.gameObject.SetActive(false);
                loudAudioSource.gameObject.SetActive(true);
                loudAudioSource.loop = true;
                loudAudioSource.Play();
            }
            else // Calm sound
            {
                loudAudioSource.gameObject.SetActive(false);
                calmAudioSource.gameObject.SetActive(true);
                calmAudioSource.loop = true;
                calmAudioSource.Play();
            }
        }
    }

    private void OnDestroy()
    {
        // Clean up listeners
        if (brightnessToggle != null)
            brightnessToggle.onValueChanged.RemoveListener(ToggleBrightness);

        if (soundToggle != null)
            soundToggle.onValueChanged.RemoveListener(ToggleSoundSensitivity);
    }
}
