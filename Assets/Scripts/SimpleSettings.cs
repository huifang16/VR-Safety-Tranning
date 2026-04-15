using UnityEngine;
using UnityEngine.UI;

public class SimpleSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle snapTurnToggle;

    public GameObject snapTurnProvider;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        bool snapTurn = PlayerPrefs.GetInt("SnapTurn", 1) == 1;

        AudioListener.volume = volume;
        volumeSlider.value = volume;
        snapTurnToggle.isOn = snapTurn;

        ApplyTurnMode(snapTurn);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        snapTurnToggle.onValueChanged.AddListener(SetTurnMode);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public void SetTurnMode(bool useSnapTurn)
    {
        ApplyTurnMode(useSnapTurn);
        PlayerPrefs.SetInt("SnapTurn", useSnapTurn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void ApplyTurnMode(bool useSnapTurn)
    {
        if (snapTurnProvider != null)
            snapTurnProvider.SetActive(useSnapTurn);
    }
}