using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;


[DefaultExecutionOrder(10)]
public class LightTime : MonoBehaviour
{
    public Transform LightsRoot;

    [Header("Day Light")]
    public Light2D DayLight;
    public Gradient DayLightGradient;

    [Header("Night Light")]
    public Light2D NightLight;
    public Gradient NightLightGradient;

    [Header("Ambient Light")]
    public Light2D AmbientLight;
    public Gradient AmbientLightGradient;

    [Header("RimLights")]
    public Light2D SunRimLight;
    public Gradient SunRimLightGradient;
    public Light2D MoonRimLight;
    public Gradient MoonRimLightGradient;

    private List<LightInterpolator> m_LightBlenders = new();


    //set to current time ratio
    public void Tick(float ratio)
    {
        UpdateLight(ratio);
    }

    private void UpdateLight(float ratio)
    {
        DayLight.color = DayLightGradient.Evaluate(ratio);
        NightLight.color = NightLightGradient.Evaluate(ratio);

#if UNITY_EDITOR
        //the test between the define will only happen in editor and not in build, as it is assumed those will be set
        //in build. But in editor we may want to test without those set. (those were added later in development so
        //some test scene didn't have those set and we wanted to be able to still test those)
        if (AmbientLight != null)
#endif
            AmbientLight.color = AmbientLightGradient.Evaluate(ratio);

#if UNITY_EDITOR
        if (SunRimLight != null)
#endif
            SunRimLight.color = SunRimLightGradient.Evaluate(ratio);

#if UNITY_EDITOR
        if (MoonRimLight != null)
#endif
            MoonRimLight.color = MoonRimLightGradient.Evaluate(ratio);

        LightsRoot.rotation = Quaternion.Euler(0, 0, 360.0f * ratio);

    }

    public static void RegisterLightBlender(LightInterpolator interpolator)
    {
#if UNITY_EDITOR
        //in the editor when not running, we find the instance manually. Less efficient but not a problem at edit time
        //allow to be able to previz shadow in editor 
        if (!Application.isPlaying)
        {
            var instance = FindObjectOfType<LightTime>();
            if (instance != null)
            {
                instance.m_LightBlenders.Add(interpolator);
            }
        }
        else
        {
#endif
            GameManager.instance.lightTime.m_LightBlenders.Add(interpolator);
#if UNITY_EDITOR
        }
#endif
    }

    public static void UnregisterLightBlender(LightInterpolator interpolator)
    {
#if UNITY_EDITOR
        //in the editor when not running, we find the instance manually. Less efficient but not a problem at edit time
        //allow to be able to previz shadow in editor 
        if (!Application.isPlaying)
        {
            var instance = FindObjectOfType<LightTime>();
            if (instance != null)
            {
                instance.m_LightBlenders.Remove(interpolator);
            }
        }
        else
        {
#endif
            if (GameManager.instance?.lightTime != null)
                GameManager.instance.lightTime.m_LightBlenders.Remove(interpolator);
#if UNITY_EDITOR
        }
#endif
    }
}