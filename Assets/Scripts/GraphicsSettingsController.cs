
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using System.IO;

public class GraphicsSettingsController : MonoBehaviour
{
    [Header("UI Controls")]
    public TMP_Dropdown textureQualityDropdown;
    public TMP_Dropdown shadowDistanceDropdown;
    public Toggle motionBlurToggle;

    [Header("Post Processing")]
    public Volume postProcessingVolume;

    private string savePath;
    private GraphicsData settings;

    [Serializable]
    public class GraphicsData
    {
        public int textureQuality = 1;
        public int shadowDistance = 1;
        public bool motionBlur = true;
    }

    private void Start()
    {
        savePath = Path.Combine(
            Application.persistentDataPath,
            "GraphicsSettings.json"
        );

        LoadSettings();

        textureQualityDropdown.SetValueWithoutNotify(
            settings.textureQuality
        );

        shadowDistanceDropdown.SetValueWithoutNotify(
            settings.shadowDistance
        );

        motionBlurToggle.SetIsOnWithoutNotify(
            settings.motionBlur
        );

        ApplySettings();

        textureQualityDropdown.onValueChanged.AddListener(
            OnTextureQualityChanged
        );

        shadowDistanceDropdown.onValueChanged.AddListener(
            OnShadowDistanceChanged
        );

        motionBlurToggle.onValueChanged.AddListener(
            OnMotionBlurChanged
        );
    }

    private void OnTextureQualityChanged(int value)
    {
        settings.textureQuality = value;
        ApplySettings();
        SaveSettings();
    }

    private void OnShadowDistanceChanged(int value)
    {
        settings.shadowDistance = value;
        ApplySettings();
        SaveSettings();
    }

    private void OnMotionBlurChanged(bool value)
    {
        settings.motionBlur = value;
        ApplySettings();
        SaveSettings();
    }

    private void ApplySettings()
    {
        // Low = 2, Medium = 1, High = 0
        QualitySettings.globalTextureMipmapLimit =
            2 - settings.textureQuality;

        // Low = 25, Medium = 50, High = 100
        float[] shadowDistances = { 25f, 50f, 100f };

        QualitySettings.shadowDistance =
            shadowDistances[Mathf.Clamp(
                settings.shadowDistance, 0, 2
            )];

        // Apply motion blur if a suitable Volume is assigned
        if (postProcessingVolume != null &&
            postProcessingVolume.profile != null &&
            postProcessingVolume.profile.TryGet(
                out MotionBlur blur
            ))
        {
            blur.active = settings.motionBlur;
        }
    }

    private void SaveSettings()
    {
        string json = JsonUtility.ToJson(settings, true);
        File.WriteAllText(savePath, json);
    }

    private void LoadSettings()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            settings = JsonUtility.FromJson<GraphicsData>(json);
        }

        if (settings == null)
        {
            settings = new GraphicsData();
        }
    }
}
