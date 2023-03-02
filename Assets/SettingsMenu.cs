using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    List<string> resolutionList = new List<string>();
    List<Resolution> resolutionList2 = new List<Resolution>();
    public TMPro.TMP_Dropdown resolutionDropdown;

    [SerializeField] Slider volumeSlider;
    void Start()
    {
        SetFullscreen(true);
        ResolutionStart();
    }

    private void ResolutionStart()
    {
        resolutions = Screen.resolutions;   // Get all resolutions availbe for the screen

        resolutionDropdown.ClearOptions();      // Clear all default set resolutions 

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)     // For that put all resolution in resoltions into a list
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height;
            if (i == 0)
            {
                resolutionList.Add(resolutionOption);
                resolutionList2.Add(resolutions[i]);
            }
            else if (resolutions[i].width != resolutions[i - 1].width && resolutions[i].height != resolutions[i - 1].height)        // Supposed to keep only the resolutions that have a refreshRate of 60hz
            {
                resolutionList.Add(resolutionOption);
                resolutionList2.Add(resolutions[i]);
            }
            if (resolutions[i].Equals(Screen.currentResolution))       // Checks if the resolution being added corresponds to the screen resolution or not
            {
                currentResolution = i;      // currentResolution takes the resolution it matched
            }
        }
        resolutionDropdown.AddOptions(resolutionList);  // Creates a Dropdown using the list of resolution
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int selectedResolutionIndex)
    {
        Resolution newResolution = resolutionList2[selectedResolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        volume = volumeSlider.value;
        audioMixer.SetFloat("volume", volume);
        Debug.Log(volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
