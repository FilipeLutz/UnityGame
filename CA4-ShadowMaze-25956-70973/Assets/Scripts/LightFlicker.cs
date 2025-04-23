using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 * LightFlicker.cs
 * 
 * This script applies a dynamic flickering effect to both a Light component and an emissive material
 * on a Renderer. It supports two flicker modes:
 * 
 * - Random: changes the light intensity at random intervals within a specified range.
 * - AnimationCurve: uses a customizable curve to animate the intensity over time.
 * 
 * The effect enhances realism for elements like torches, candles, and lamps in dark environments.
 * Includes a custom editor for toggling parameters directly from the Unity Inspector.
 * 
 * Developed for Unity projects requiring atmospheric lighting.
 * It was developed following the tutorial (https://learn.unity.com/tutorial/make-health-bar-with-UItoolkit?start=true#).
 */

public class LightFlicker : MonoBehaviour
{
    // Defines the flickering style
    public enum FlickerMode
    {
        Random,
        AnimationCurve
    }

    // References to the Light and Renderer components
    public Light flickeringLight;
    public Renderer flickeringRenderer;
    public FlickerMode flickerMode;

    // Flicker parameters
    public float lightIntensityMin = 1.25f;
    public float lightIntensityMax = 2.25f;
    public float flickerDuration = 0.075f;

    // Animation curve for flicker intensity
    public AnimationCurve intensityCurve;

    // Private variables for internal state
    Material m_FlickeringMaterial;
    Color m_EmissionColor;
    float m_Timer;
    float m_FlickerLightIntensity;

    // Shader property ID for emission color
    static readonly int k_EmissionColorID = Shader.PropertyToID(k_EmissiveColorName);

    // Constants for emission color and intensity mapping
    const string k_EmissiveColorName = "_EmissionColor";
    const string k_EmissionName = "_Emission";
    const float k_LightIntensityToEmission = 2f / 3f; // How light intensity maps to emission brightness

    void Start()
    {
        // Cache material and enable emission
        m_FlickeringMaterial = flickeringRenderer.material;
        m_FlickeringMaterial.EnableKeyword(k_EmissionName);
        m_EmissionColor = m_FlickeringMaterial.GetColor(k_EmissionColorID);
    }

    void Update()
    {
        // Track time
        m_Timer += Time.deltaTime;

        // Select mode behavior
        if (flickerMode == FlickerMode.Random)
        {
            if (m_Timer >= flickerDuration)
                ChangeRandomFlickerLightIntensity();
        }
        else if (flickerMode == FlickerMode.AnimationCurve)
        {
            ChangeAnimatedFlickerLightIntensity();
        }

        // Apply intensity to light and emission
        flickeringLight.intensity = m_FlickerLightIntensity;
        m_FlickeringMaterial.SetColor(k_EmissionColorID, m_EmissionColor * m_FlickerLightIntensity * k_LightIntensityToEmission);
    }

    // Picks a random intensity
    void ChangeRandomFlickerLightIntensity()
    {
        m_FlickerLightIntensity = Random.Range(lightIntensityMin, lightIntensityMax);
        m_Timer = 0f;
    }

    // Uses an animation curve for intensity over time
    void ChangeAnimatedFlickerLightIntensity()
    {
        m_FlickerLightIntensity = intensityCurve.Evaluate(m_Timer);

        // Loop the curve if time exceeds end
        if (m_Timer >= intensityCurve[intensityCurve.length - 1].time)
            m_Timer = intensityCurve[0].time;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(LightFlicker))]
public class LightFlickerEditor : Editor
{
    // Serialized fields for drawing the custom inspector
    SerializedProperty m_ScriptProp;
    SerializedProperty m_FlickeringLightProp;
    SerializedProperty m_FlickeringRendererProp;
    SerializedProperty m_FlickerModeProp;
    SerializedProperty m_LightIntensityMinProp;
    SerializedProperty m_LightIntensityMaxProp;
    SerializedProperty m_FlickerDurationProp;
    SerializedProperty m_IntensityCurveProp;

    void OnEnable()
    {
        // Link serialized properties to actual fields
        m_ScriptProp = serializedObject.FindProperty("m_Script");
        m_FlickeringLightProp = serializedObject.FindProperty("flickeringLight");
        m_FlickeringRendererProp = serializedObject.FindProperty("flickeringRenderer");
        m_FlickerModeProp = serializedObject.FindProperty("flickerMode");
        m_LightIntensityMinProp = serializedObject.FindProperty("lightIntensityMin");
        m_LightIntensityMaxProp = serializedObject.FindProperty("lightIntensityMax");
        m_FlickerDurationProp = serializedObject.FindProperty("flickerDuration");
        m_IntensityCurveProp = serializedObject.FindProperty("intensityCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Show the script reference in a disabled field
        GUI.enabled = false;
        EditorGUILayout.PropertyField(m_ScriptProp);
        GUI.enabled = true;

        // Draw common fields
        EditorGUILayout.PropertyField(m_FlickeringLightProp);
        EditorGUILayout.PropertyField(m_FlickeringRendererProp);
        EditorGUILayout.PropertyField(m_FlickerModeProp);

        // Conditional UI
        if (m_FlickerModeProp.enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField(m_LightIntensityMinProp);
            EditorGUILayout.PropertyField(m_LightIntensityMaxProp);
            EditorGUILayout.PropertyField(m_FlickerDurationProp);
        }
        else if (m_FlickerModeProp.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(m_IntensityCurveProp);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif