using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    void Start()
    {
        SetFullscreen(true);
        ResolutionStart();
    }

    private void ResolutionStart()
    {
        Debug.Log("Start method is called");
        resolutions = Screen.resolutions;   // Get all resolutions availbe for the screen

        resolutionDropdown.ClearOptions();      // Clear all default set resolutions 

        List<string> resolutionList = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)     // For that put all resolution in resoltions into a list
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height + " - " + resolutions[i].refreshRate + "hz";
            //if (resolutions[i].refreshRate == 60)        // Supposed to keep only the resolutions that have a refreshRate of 60hz
            {
                resolutionList.Add(resolutionOption);
            }
            //if (resolutions[i].width == Screen.width &&       
            //  resolutions[i].height == Screen.height)
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
        Resolution newResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
        Debug.Log("New resolution : " + newResolution.width + " x " + newResolution.height);
    }

    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen)
        {
            Debug.Log("Enabling fullscreen");
        }
        else
        {
            Debug.Log("Deactivating fullscreen");
        }
    }
}
