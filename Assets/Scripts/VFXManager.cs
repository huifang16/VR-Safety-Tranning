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
            z.particles.Play();
    }

    public void SetSmokeIntensity(string zoneId, float t) {
        if (!_zones.TryGetValue(zoneId, out var z)) return;
        var main = z.particles.main;
        main.startColor = new ParticleSystem.MinMaxGradient(
            new Color(0.5f, 0.5f, 0.5f, t));
    }

    public void ShowFire(string zoneId) {
        neighbourFireVFX.transform.position = 
            _zones[zoneId].particles.transform.position;
        neighbourFireVFX.Play();
        ShowSmoke(zoneId);
    }

    public void StartSmokeRamp(string zoneId) =>
        StartCoroutine(SmokeRamp(zoneId));

    IEnumerator SmokeRamp(string zoneId) {
        float t = 0;
        while (t < 1f) {
            t += Time.deltaTime * 0.15f; // slow build over ~7 seconds
            SetSmokeIntensity(zoneId, t);
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