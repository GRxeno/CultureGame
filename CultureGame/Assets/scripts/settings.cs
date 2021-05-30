using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System;

public class settings : MonoBehaviour
{
    public AudioMixer mixer;
    //public AudioMixer sfx_mixer;

    public Slider mainSlider;
    public Slider sfxSlider;
    public Slider sensSlider;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    
    public Toggle fullscreenToggle;
    public Toggle vsyncToggle;

    public TMP_Text main_percentageText;
    public TMP_Text sens_percentageText;
    public TMP_Text sfx_percentageText;

   List<Resolution> resolutions;
    float curVolume;
    float curSFXVolume;
    int curResIndex;

    void Awake()
    {

        //Debug.Log("Finding resolutions!");
        resolutions = GetResolutions();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Count; i++)
        {
            
            string option = resolutions[i].width + " x " + resolutions[i].height;
            //if(!options.Contains(option))
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                curResIndex = i;
                
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = curResIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public static List<Resolution> GetResolutions()
    {
        //Filters out all resolutions with low refresh rate:
        Resolution[] resolutions = Screen.resolutions;
        HashSet<Tuple<int, int>> uniqResolutions = new HashSet<Tuple<int, int>>();
        Dictionary<Tuple<int, int>, int> maxRefreshRates = new Dictionary<Tuple<int, int>, int>();
        for (int i = 0; i < resolutions.GetLength(0); i++)
        {
            //Add resolutions (if they are not already contained)
            Tuple<int, int> resolution = new Tuple<int, int>(resolutions[i].width, resolutions[i].height);
            uniqResolutions.Add(resolution);
            //Get highest framerate:
            if (!maxRefreshRates.ContainsKey(resolution))
            {
                maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
            }
            else
            {
                maxRefreshRates[resolution] = resolutions[i].refreshRate;
            }
        }
        //Build resolution list:
        List<Resolution> uniqResolutionsList = new List<Resolution>(uniqResolutions.Count);
        foreach (Tuple<int, int> resolution in uniqResolutions)
        {
            Resolution newResolution = new Resolution();
            newResolution.width = resolution.Item1;
            newResolution.height = resolution.Item2;
            if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
            {
                newResolution.refreshRate = refreshRate;
            }
            uniqResolutionsList.Add(newResolution);
        }
        return uniqResolutionsList;
    }

    private void Start()
    {
        // Set music slider to current music volume
        mixer.GetFloat("MusicV", out curVolume);
        curVolume = curVolume / 20;
        curVolume = Mathf.Pow(10, curVolume);
        main_percentageText.text = Mathf.RoundToInt(curVolume * 100) + "%";
        mainSlider.value = curVolume;
        // Set resolutions dropdown to correct resolutions
        //resolutionDropdown.value = curResIndex;
        //resolutionDropdown.RefreshShownValue();
        // Set Fullscreen Toglle to correct value
        fullscreenToggle.isOn = Screen.fullScreen;
        // Set Quallity to correct value
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
        // Set Sensitivity to correct value
        sens_percentageText.text = Mathf.RoundToInt(MouseLook.mouseSens / 10) + "%";
        sensSlider.value = MouseLook.mouseSens;
        // Set Vsync Toggle to correct value
        if(QualitySettings.vSyncCount != 0)
            vsyncToggle.isOn = true;
        else
            vsyncToggle.isOn = false;
        // Set SFX slider to current music volume
        mixer.GetFloat("SFXV", out curSFXVolume);
        curSFXVolume = curSFXVolume / 20;
        curSFXVolume = Mathf.Pow(10, curSFXVolume);
        sfx_percentageText.text = Mathf.RoundToInt(curSFXVolume * 100) + "%";
        sfxSlider.value = curSFXVolume;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicV", Mathf.Log10(sliderValue) * 20);
        main_percentageText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
    }

    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXV", Mathf.Log10(sliderValue) * 20);
        sfx_percentageText.text = Mathf.RoundToInt(sliderValue * 100) + "%";
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void setVSync(bool isVsync)
    {
        if (isVsync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetSensitivity(float sliderValue)
    {
        MouseLook.mouseSens = sliderValue;
        sens_percentageText.text = Mathf.RoundToInt(sliderValue / 10) + "%";
    }


    public void close()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }


}
