// VFXManager.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VFXManager : MonoBehaviour {
    public static VFXManager Instance;

    [System.Serializable]
    public struct SmokeZone {
        public string id;
        public ParticleSystem particles;
        public Light ambientLight; // optional orange tint
    }

    public SmokeZone[] smokeZones;
    public ParticleSystem neighbourFireVFX;
    public ParticleSystem outdoorChaosVFX;
    public GameObject highlightArrow; // UI arrow that guides player

    Dictionary<string, SmokeZone> _zones;

    void Awake() {
        Instance = this;
        _zones = new();
        foreach (var z in smokeZones) _zones[z.id] = z;
    }

    public void ShowSmoke(string zoneId) {
        if (_zones.TryGetValue(zoneId, out var z))
        {
            z.particles.Play();
        }
    }
    
    public void DisableSmoke(string zoneId)
    {
        if (_zones.TryGetValue(zoneId, out var z))
        {
            z.particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        
    }

    public void SetSmokeIntensity(string zoneId, float intensity) {
        if (!_zones.TryGetValue(zoneId, out var z))
        {
            Debug.LogError("no smoke found");
            return;
        }
        
        intensity = Mathf.Clamp01(intensity);
        var main = z.particles.main;
        
        float v = Mathf.Lerp(0.1f, 0.9f, intensity);
        Color smokeColor = new Color(v, v, v, v);
        main.startColor = smokeColor;
    }

    public void ShowFire(string zoneId) {
        neighbourFireVFX.gameObject.SetActive(true);
    }

    public void StartSmokeRamp(string zoneId) =>
        StartCoroutine(SmokeRamp(zoneId));

    IEnumerator SmokeRamp(string zoneId) {
        float t = 0;
        while (t < 1f) {
            t += Time.deltaTime * 0.05f; // slow build over ~20 seconds
            SetSmokeIntensity(zoneId, t);
            Debug.Log("Start smoke ramp");
            yield return null;
        }
    }

    public void ShowOutdoorChaos() => outdoorChaosVFX.Play();

    public void SetHighlight(Transform target) {
        highlightArrow.SetActive(target != null);
        if (target != null)
            highlightArrow.transform.position = target.position + Vector3.up * 2f;
    }
}