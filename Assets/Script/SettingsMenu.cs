using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    Resolution[] _resolutions;
    List<string> _resolutionList = new List<string>();
    List<Resolution> _resolutionList2 = new List<Resolution>();
    public TMPro.TMP_Dropdown resolutionDropdown;

    public AudioMixer audioMixer;

    [SerializeField] Slider _volumeSlider;
    void Start()
    {
        SetFullscreen(true);
        ResolutionStart();
    }

    private void ResolutionStart()
    {
        _resolutions = Screen.resolutions;   // Get all resolutions availbe for the screen

        resolutionDropdown.ClearOptions();      // Clear all default set resolutions 

        int currentResolution = 0;
        for (int i = 0; i < _resolutions.Length; i++)     // For that put all resolution in resoltions into a list
        {
            string resolutionOption = _resolutions[i].width + " x " + _resolutions[i].height;
            if (i == 0)
            {
                _resolutionList.Add(resolutionOption);
                _resolutionList2.Add(_resolutions[i]);
            }
            else if (_resolutions[i].width != _resolutions[i - 1].width && _resolutions[i].height != _resolutions[i - 1].height)        // Supposed to keep only the resolutions that have a refreshRate of 60hz
            {
                _resolutionList.Add(resolutionOption);
                _resolutionList2.Add(_resolutions[i]);
            }
            if (_resolutions[i].Equals(Screen.currentResolution))       // Checks if the resolution being added corresponds to the screen resolution or not
            {
                currentResolution = i;      // currentResolution takes the resolution it matched
            }
        }
        resolutionDropdown.AddOptions(_resolutionList);  // Creates a Dropdown using the list of resolution
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int selectedResolutionIndex)
    {
        Resolution newResolution = _resolutionList2[selectedResolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    public void SetVolume()
    {
        float volume = _volumeSlider.value;
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
