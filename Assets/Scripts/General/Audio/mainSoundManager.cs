using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainSoundManager : MonoBehaviour
{
    [SerializeField] Slider volSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            Load();
        }
        else {
            Load();
        }
        
    }

    public void changeVolume() {
        AudioListener.volume = volSlider.value;
        Save();
    }

    public void Load() {
        volSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void Save() {
        PlayerPrefs.SetFloat("volume", volSlider.value);
    }

}
