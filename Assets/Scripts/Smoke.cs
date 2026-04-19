using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SmokeEffect
{
    public string smokeName;
    public ParticleSystem particleSystem;
}

public class Smoke : MonoBehaviour
{
    public static Smoke Instance;

    [Header("Smoke Effects")]
    public List<SmokeEffect> smokeEffects = new List<SmokeEffect>();

    private Dictionary<string, ParticleSystem> smokeDict = new Dictionary<string, ParticleSystem>();

    void Start()
    {
        ShowSmoke("FrontDoorSmoke");
    }

    void Awake()
    {
        Instance = this;

        smokeDict.Clear();

        foreach (SmokeEffect smoke in smokeEffects)
        {
            if (smoke != null && smoke.particleSystem != null && !string.IsNullOrEmpty(smoke.smokeName))
            {
                if (!smokeDict.ContainsKey(smoke.smokeName))
                {
                    smokeDict.Add(smoke.smokeName, smoke.particleSystem);
                    smoke.particleSystem.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ShowSmoke(string name)
    {
        if (!smokeDict.ContainsKey(name))
        {
            Debug.LogWarning("Smoke not found: " + name);
            return;
        }

        ParticleSystem ps = smokeDict[name];

        ps.gameObject.SetActive(true);
        ps.Clear();
        ps.Play();

        Debug.Log("Show Smoke: " + name);
    }

    public void HideSmoke(string name)
    {
        if (!smokeDict.ContainsKey(name))
        {
            Debug.LogWarning("Smoke not found: " + name);
            return;
        }

        ParticleSystem ps = smokeDict[name];

        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ps.gameObject.SetActive(false);

        Debug.Log("Hide Smoke: " + name);
    }

    public void SetSmokeIntensity(string name, float rate)
    {
        if (!smokeDict.ContainsKey(name))
        {
            Debug.LogWarning("Smoke not found: " + name);
            return;
        }

        ParticleSystem ps = smokeDict[name];
        var emission = ps.emission;
        emission.rateOverTime = rate;

        Debug.Log("Set Smoke Intensity: " + name + " = " + rate);
    }
}